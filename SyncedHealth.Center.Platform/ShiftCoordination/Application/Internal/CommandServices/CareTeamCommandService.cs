using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;

/// <summary>
/// Represents the care team command service in the CortiSense Platform.
/// </summary>
public class CareTeamCommandService(
    ICareTeamRepository careTeamRepository,
    SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork)
    : ICareTeamCommandService
{
    private readonly SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService _auditLogCommandService = auditLogCommandService;
    public async Task<Result<CareTeam>> Handle(
        CreateCareTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var isActive = command.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);

        if (isActive && await careTeamRepository.ExistsActiveBySupervisorIdAsync(
                command.SupervisorId,
                null,
                cancellationToken))
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.SupervisorAlreadyAssignedToActiveTeam,
                "The supervisor is already assigned to another active team."
            );
        }

        var careTeam = new CareTeam(command);

        try
        {
            await careTeamRepository.AddAsync(careTeam, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                command.OrganizationId,
                command.SupervisorId, // Assuming Supervisor is the actor for now, or just default to 1 since we don't have the actual user id who performed this action in the command
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.TeamCreated,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.CareTeam,
                careTeam.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ShiftCoordination,
                $"Care team {careTeam.Name} created."
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<CareTeam>.Success(careTeam);
        }
        catch (OperationCanceledException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while creating the care team."
            );
        }
        catch (Exception)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while creating the care team."
            );
        }
    }

    public async Task<Result<CareTeam>> Handle(
        UpdateCareTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var careTeam = await careTeamRepository.FindByIdAsync(command.Id, cancellationToken);

        if (careTeam is null)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.CareTeamNotFound,
                "The specified care team was not found."
            );
        }

        var isActive = command.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);

        if (isActive && await careTeamRepository.ExistsActiveBySupervisorIdAsync(
                command.SupervisorId,
                command.Id,
                cancellationToken))
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.SupervisorAlreadyAssignedToActiveTeam,
                "The supervisor is already assigned to another active team."
            );
        }

        careTeam.Update(command);

        try
        {
            careTeamRepository.Update(careTeam);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                careTeam.OrganizationId,
                command.SupervisorId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.TeamUpdated,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.CareTeam,
                careTeam.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ShiftCoordination,
                $"Care team {careTeam.Name} updated."
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<CareTeam>.Success(careTeam);
        }
        catch (OperationCanceledException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while updating the care team."
            );
        }
        catch (Exception)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while updating the care team."
            );
        }
    }

    public async Task<Result<CareTeam>> Handle(
        DeleteCareTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var careTeam = await careTeamRepository.FindByIdAsync(command.Id, cancellationToken);

        if (careTeam is null)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.CareTeamNotFound,
                "The specified care team was not found."
            );
        }

        try
        {
            careTeamRepository.Remove(careTeam);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                careTeam.OrganizationId,
                careTeam.SupervisorId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.TeamDeleted,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Warning,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.CareTeam,
                careTeam.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ShiftCoordination,
                $"Care team {careTeam.Name} deleted."
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<CareTeam>.Success(careTeam);
        }
        catch (OperationCanceledException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while deleting the care team."
            );
        }
        catch (Exception)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while deleting the care team."
            );
        }
    }
}