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
using SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.CommandServices;

public class RiskAssessmentCommandService(
    IRiskAssessmentRepository riskAssessmentRepository,
    IVitalSignAnomalyCommandService vitalSignAnomalyCommandService,
    IClinicalAlertCommandService clinicalAlertCommandService,
    IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ClinicalRiskAssessmentMessages> localizer)
    : IRiskAssessmentCommandService
{
    private readonly IStringLocalizer<ClinicalRiskAssessmentMessages> _localizer = localizer;

    private static readonly string[] ValidRiskLevels = ["LOW", "MEDIUM", "HIGH", "CRITICAL"];

    public async Task<Result<RiskAssessment>> Handle(
        CreateRiskAssessmentCommand command,
        CancellationToken cancellationToken)
    {
        if (command.OrganizationId <= 0 || command.UserId <= 0)
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidOrganizationOrUser"]);

        if (command.FatigueLevel is < 0 or > 100)
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidFatigueLevel"]);

        if (!ValidRiskLevels.Contains(command.RiskLevel.ToUpperInvariant()))
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.InvalidRiskLevel,
                _localizer["InvalidRiskLevel"]);

        if (command.HeartRate <= 0 || command.Hrv <= 0)
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidHeartRateOrHrv"]);

        var riskAssessment = new RiskAssessment(command);

        try
        {
            await riskAssessmentRepository.AddAsync(riskAssessment, cancellationToken);
            
            // Generate real-time anomaly, alert, and audit if thresholds are exceeded
            if (command.HeartRate > 100 || command.FatigueLevel > 80 || command.RiskLevel.ToUpperInvariant() == "CRITICAL" || command.RiskLevel.ToUpperInvariant() == "HIGH")
            {
                var anomalyCommand = new CreateVitalSignAnomalyCommand(
                    command.OrganizationId,
                    command.UserId,
                    "HEART_RATE_SPIKE",
                    "CRITICAL",
                    "OPEN",
                    command.HeartRate.ToString(),
                    "100",
                    "High heart rate or fatigue detected during risk assessment.",
                    DateTimeOffset.UtcNow
                );
                await vitalSignAnomalyCommandService.Handle(anomalyCommand, cancellationToken);

                var alertCommand = new CreateClinicalAlertCommand(
                    command.OrganizationId,
                    command.UserId,
                    "CRITICAL",
                    "OPEN",
                    "Critical biometric risk detected, immediate review required.",
                    DateTimeOffset.UtcNow
                );
                await clinicalAlertCommandService.Handle(alertCommand, cancellationToken);
            }

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<RiskAssessment>.Success(riskAssessment);
        }
        catch (OperationCanceledException)
        {
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["CreateRiskAssessmentDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<RiskAssessment>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedRiskAssessmentError"]);
        }
    }
}