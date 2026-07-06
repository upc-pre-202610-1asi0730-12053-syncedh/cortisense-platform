using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Errors;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.Internal.CommandServices;

public class RecoveryPlanCommandService(
    IRecoveryPlanRepository recoveryPlanRepository,
    SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork) : IRecoveryPlanCommandService
{
    private readonly SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService _auditLogCommandService = auditLogCommandService;
    public async Task<Result<RecoveryPlan>> Handle(
        IssueRecoveryRecommendationCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.MedicalStaffId <= 0 || string.IsNullOrWhiteSpace(command.Description))
                return Result<RecoveryPlan>.Failure(
                    StaffRecoveryError.InvalidRecoveryPlanData,
                    "Invalid recovery plan data.");

            if (command.SuggestedRestDays <= 0)
                return Result<RecoveryPlan>.Failure(
                    StaffRecoveryError.InvalidSuggestedRestDays,
                    "Suggested rest days must be greater than zero.");

            var recoveryPlan = new RecoveryPlan(command);

            await recoveryPlanRepository.AddAsync(recoveryPlan, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                1, // OrganizationId isn't on RecoveryPlan directly, but let's assume 1 for now or pull from user
                command.MedicalStaffId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.PreventiveActionCreated,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.PreventiveAction,
                recoveryPlan.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.StaffRecovery,
                $"Preventive action issued for User {command.MedicalStaffId}"
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<RecoveryPlan>.Success(recoveryPlan);
        }
        catch (OperationCanceledException)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.DatabaseError,
                "A database error occurred while creating the recovery plan.");
        }
        catch (Exception)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while creating the recovery plan.");
        }
    }

    public async Task<Result<RecoveryPlan>> Handle(
        AcceptRecoveryPlanCommand command,
        CancellationToken cancellationToken = default)
    {
        return await UpdateStatusAsync(command.Id, "ACCEPTED", cancellationToken);
    }

    public async Task<Result<RecoveryPlan>> Handle(
        RejectRecoveryPlanCommand command,
        CancellationToken cancellationToken = default)
    {
        return await UpdateStatusAsync(command.Id, "REJECTED", cancellationToken);
    }

    public async Task<Result<RecoveryPlan>> Handle(
        ConfirmRecoveryCommand command,
        CancellationToken cancellationToken = default)
    {
        return await UpdateStatusAsync(command.Id, "COMPLETED", cancellationToken);
    }

    private async Task<Result<RecoveryPlan>> UpdateStatusAsync(int id, string status, CancellationToken cancellationToken)
    {
        var recoveryPlan = await recoveryPlanRepository.FindByIdAsync(id, cancellationToken);
        if (recoveryPlan == null)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.RecoveryPlanNotFound,
                "Recovery plan not found.");
        }

        recoveryPlan.UpdateStatus(status);

        try
        {
            recoveryPlanRepository.Update(recoveryPlan);
            await unitOfWork.CompleteAsync(cancellationToken);

            if (status == "ACCEPTED" || status == "REJECTED" || status == "COMPLETED")
            {
                var auditType = status == "ACCEPTED" ? SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.PreventiveActionAccepted : 
                                status == "COMPLETED" ? SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.PreventiveActionCompleted : 
                                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.PreventiveActionCompleted; // fallback

                // Audit Log
                var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                    1, 
                    recoveryPlan.MedicalStaffId,
                    auditType,
                    SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                    SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.PreventiveAction,
                    recoveryPlan.Id,
                    SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.StaffRecovery,
                    $"Preventive action {status.ToLower()} for User {recoveryPlan.MedicalStaffId}."
                );
                await _auditLogCommandService.Handle(auditCommand, cancellationToken);
            }

            return Result<RecoveryPlan>.Success(recoveryPlan);
        }
        catch (OperationCanceledException)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.OperationCancelled,
                "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.DatabaseError,
                "A database error occurred while updating the recovery plan.");
        }
        catch (Exception)
        {
            return Result<RecoveryPlan>.Failure(
                StaffRecoveryError.InternalServerError,
                "An unexpected error occurred while updating the recovery plan.");
        }
    }
}