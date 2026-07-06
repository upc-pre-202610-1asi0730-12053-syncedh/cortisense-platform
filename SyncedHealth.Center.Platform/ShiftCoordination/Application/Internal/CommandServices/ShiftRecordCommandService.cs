using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Resources;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;

public class ShiftRecordCommandService(
    IShiftRecordRepository shiftRecordRepository,
    SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ShiftCoordinationMessages> localizer)
    : IShiftRecordCommandService
{
    private readonly IStringLocalizer<ShiftCoordinationMessages> _localizer = localizer;
    private readonly SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService _auditLogCommandService = auditLogCommandService;

    private static readonly string[] ValidTypes = ["DAY", "NIGHT", "EMERGENCY"];

    private static readonly string[] ValidStatuses = ["SCHEDULED", "IN_PROGRESS", "COMPLETED", "CANCELLED"];

    public async Task<Result<ShiftRecord>> Handle(
        CreateShiftRecordCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = ValidateCreateCommand(command);
        if (validationResult is not null) return validationResult;

        var shiftRecord = new ShiftRecord(command);

        try
        {
            await shiftRecordRepository.AddAsync(shiftRecord, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (OperationCanceledException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.DatabaseError,
                _localizer["CreateShiftRecordDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InternalServerError,
                _localizer["UnexpectedShiftRecordCreateError"]);
        }
    }

    public async Task<Result<ShiftRecord>> Handle(
        UpdateShiftRecordStatusCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.Id, cancellationToken);

        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.ShiftRecordNotFound,
                _localizer["ShiftRecordNotFound"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftStatus,
                _localizer["InvalidShiftStatus"]);

        var normalizedStatus = command.Status.ToUpperInvariant();

        if (normalizedStatus == "COMPLETED" && shiftRecord.CheckInAt is null && command.CheckInAt is null)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftRecordData,
                _localizer["CheckInRequiredBeforeCheckout"]);

        shiftRecord.UpdateStatus(command.Status, command.CheckInAt, command.CheckOutAt);

        try
        {
            shiftRecordRepository.Update(shiftRecord);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (OperationCanceledException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.DatabaseError,
                _localizer["UpdateShiftRecordDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InternalServerError,
                _localizer["UnexpectedShiftRecordUpdateError"]);
        }
    }

    public async Task<Result<ShiftRecord>> Handle(
        CheckInShiftRecordCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.Id, cancellationToken);

        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.ShiftRecordNotFound,
                _localizer["ShiftRecordNotFound"]);

        shiftRecord.UpdateStatus("IN_PROGRESS", command.CheckInAt, null);

        try
        {
            shiftRecordRepository.Update(shiftRecord);
            await unitOfWork.CompleteAsync(cancellationToken);
            
            // Audit
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                shiftRecord.OrganizationId,
                shiftRecord.UserId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.ShiftCheckIn,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.Shift,
                shiftRecord.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ShiftCoordination,
                $"User {shiftRecord.UserId} checked into Shift #{shiftRecord.Id}"
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (OperationCanceledException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.DatabaseError,
                _localizer["UpdateShiftRecordDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InternalServerError,
                _localizer["UnexpectedShiftRecordUpdateError"]);
        }
    }

    public async Task<Result<ShiftRecord>> Handle(
        CheckOutShiftRecordCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.Id, cancellationToken);

        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.ShiftRecordNotFound,
                _localizer["ShiftRecordNotFound"]);

        if (shiftRecord.CheckInAt is null)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftRecordData,
                _localizer["CheckInRequiredBeforeCheckout"]);

        shiftRecord.UpdateStatus("COMPLETED", null, command.CheckOutAt);

        try
        {
            shiftRecordRepository.Update(shiftRecord);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (OperationCanceledException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.DatabaseError,
                _localizer["UpdateShiftRecordDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InternalServerError,
                _localizer["UnexpectedShiftRecordUpdateError"]);
        }
    }
    public async Task<Result<ShiftRecord>> Handle(
        EvaluateCriticalShiftCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.ShiftRecordId, cancellationToken);
        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(ShiftCoordinationError.ShiftRecordNotFound, _localizer["ShiftRecordNotFound"]);

        // Logic for evaluating shift risk goes here (calls external service or applies policy)
        // For MVP, we return success (evaluation passed)
        return Result<ShiftRecord>.Success(shiftRecord);
    }

    public async Task<Result<ShiftRecord>> Handle(
        BlockShiftCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.ShiftRecordId, cancellationToken);
        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(ShiftCoordinationError.ShiftRecordNotFound, _localizer["ShiftRecordNotFound"]);

        shiftRecord.Block();
        
        try
        {
            shiftRecordRepository.Update(shiftRecord);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(ShiftCoordinationError.InternalServerError, _localizer["UnexpectedShiftRecordUpdateError"]);
        }
    }

    public async Task<Result<ShiftRecord>> Handle(
        ReassignShiftCommand command,
        CancellationToken cancellationToken)
    {
        var shiftRecord = await shiftRecordRepository.FindByIdAsync(command.ShiftRecordId, cancellationToken);
        if (shiftRecord is null)
            return Result<ShiftRecord>.Failure(ShiftCoordinationError.ShiftRecordNotFound, _localizer["ShiftRecordNotFound"]);

        var oldUserId = shiftRecord.UserId;
        shiftRecord.Reassign(command.NewUserId);
        
        try
        {
            shiftRecordRepository.Update(shiftRecord);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                shiftRecord.OrganizationId,
                command.NewUserId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.TeamUpdated,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Warning,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.Shift,
                shiftRecord.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ShiftCoordination,
                $"Shift #{shiftRecord.Id} reassigned from User {oldUserId} to User {command.NewUserId}"
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<ShiftRecord>.Success(shiftRecord);
        }
        catch (Exception)
        {
            return Result<ShiftRecord>.Failure(ShiftCoordinationError.InternalServerError, _localizer["UnexpectedShiftRecordUpdateError"]);
        }
    }

    private Result<ShiftRecord>? ValidateCreateCommand(CreateShiftRecordCommand command)
    {
        if (command.OrganizationId <= 0 || command.UserId <= 0 || command.WorkAreaId <= 0)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftRecordData,
                _localizer["InvalidOrganizationUserOrWorkArea"]);

        if (!ValidTypes.Contains(command.Type.ToUpperInvariant()))
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftType,
                _localizer["InvalidShiftType"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftStatus,
                _localizer["InvalidShiftStatus"]);

        if (command.ScheduledStart == default || command.ScheduledEnd == default)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftSchedule,
                _localizer["InvalidShiftSchedule"]);

        if (command.ScheduledEnd <= command.ScheduledStart)
            return Result<ShiftRecord>.Failure(
                ShiftCoordinationError.InvalidShiftSchedule,
                _localizer["ScheduledEndMustBeAfterStart"]);

        return null;
    }
}