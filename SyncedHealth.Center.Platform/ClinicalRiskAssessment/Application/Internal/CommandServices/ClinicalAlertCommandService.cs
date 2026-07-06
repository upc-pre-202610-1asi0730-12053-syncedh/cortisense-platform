using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Resources;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.CommandServices;

public class ClinicalAlertCommandService(
    IClinicalAlertRepository clinicalAlertRepository,
    SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ClinicalRiskAssessmentMessages> localizer)
    : IClinicalAlertCommandService
{
    private readonly IStringLocalizer<ClinicalRiskAssessmentMessages> _localizer = localizer;
    private readonly SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices.IAuditLogCommandService _auditLogCommandService = auditLogCommandService;

    private static readonly string[] ValidSeverities = ["LOW", "MEDIUM", "HIGH", "CRITICAL"];
    private static readonly string[] ValidStatuses = ["OPEN", "ACKNOWLEDGED", "RESOLVED"];

    public async Task<Result<ClinicalAlert>> Handle(
        CreateClinicalAlertCommand command,
        CancellationToken cancellationToken)
    {
        if (command.OrganizationId <= 0 || command.UserId <= 0)
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidOrganizationOrUser"]);

        if (!ValidSeverities.Contains(command.Severity.ToUpperInvariant()))
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InvalidSeverity,
                _localizer["InvalidSeverity"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InvalidStatus,
                _localizer["InvalidClinicalAlertStatus"]);

        if (string.IsNullOrWhiteSpace(command.Message))
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["MessageRequired"]);

        var existingAlerts = await clinicalAlertRepository.FindByUserIdAsync(command.UserId, cancellationToken);
        var openAlert = existingAlerts.FirstOrDefault(a => a.Status == "OPEN" || a.Status == "ACKNOWLEDGED");
        if (openAlert != null)
        {
            // Do not recreate, avoid spamming
            return Result<ClinicalAlert>.Success(openAlert);
        }

        var clinicalAlert = new ClinicalAlert(command);

        try
        {
            await clinicalAlertRepository.AddAsync(clinicalAlert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                command.OrganizationId,
                command.UserId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.AlertCreated,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Warning,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.ClinicalAlert,
                clinicalAlert.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ClinicalRiskAssessment,
                $"Alert {clinicalAlert.Severity} created for User {command.UserId}."
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<ClinicalAlert>.Success(clinicalAlert);
        }
        catch (OperationCanceledException)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["CreateClinicalAlertDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedClinicalAlertCreateError"]);
        }
    }

    public async Task<Result<ClinicalAlert>> Handle(
        UpdateClinicalAlertStatusCommand command,
        CancellationToken cancellationToken)
    {
        var clinicalAlert = await clinicalAlertRepository.FindByIdAsync(command.Id, cancellationToken);

        if (clinicalAlert is null)
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.ClinicalAlertNotFound,
                _localizer["ClinicalAlertNotFound"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InvalidStatus,
                _localizer["InvalidClinicalAlertStatus"]);

        clinicalAlert.UpdateStatus(command.Status, command.ResolvedBy);

        try
        {
            clinicalAlertRepository.Update(clinicalAlert);
            await unitOfWork.CompleteAsync(cancellationToken);

            // Audit Log
            var auditCommand = new SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands.CreateAuditLogCommand(
                clinicalAlert.OrganizationId,
                command.ResolvedBy ?? clinicalAlert.UserId,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditLogType.AlertResolved,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditSeverity.Info,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditResourceType.ClinicalAlert,
                clinicalAlert.Id,
                SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects.EAuditActionSource.ClinicalRiskAssessment,
                $"Alert status updated to {command.Status} for User {clinicalAlert.UserId}."
            );
            await _auditLogCommandService.Handle(auditCommand, cancellationToken);

            return Result<ClinicalAlert>.Success(clinicalAlert);
        }
        catch (OperationCanceledException)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["UpdateClinicalAlertDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<ClinicalAlert>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedClinicalAlertUpdateError"]);
        }
    }
}