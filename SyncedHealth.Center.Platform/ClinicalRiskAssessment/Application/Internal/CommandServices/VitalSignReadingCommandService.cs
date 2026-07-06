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

public class VitalSignReadingCommandService(
    IVitalSignReadingRepository vitalSignReadingRepository,
    IVitalSignAnomalyCommandService vitalSignAnomalyCommandService,
    IClinicalAlertCommandService clinicalAlertCommandService,
    IAuditLogCommandService auditLogCommandService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ClinicalRiskAssessmentMessages> localizer)
    : IVitalSignReadingCommandService
{
    private readonly IStringLocalizer<ClinicalRiskAssessmentMessages> _localizer = localizer;

    private static readonly string[] ValidSensorStatuses = ["ACTIVE", "DISCONNECTED", "ERROR"];

    public async Task<Result<VitalSignReading>> Handle(
        CreateVitalSignReadingCommand command,
        CancellationToken cancellationToken)
    {
        if (command.OrganizationId <= 0 || command.UserId <= 0)
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidOrganizationOrUser"]);

        if (command.HeartRate <= 0 || command.Hrv <= 0)
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidHeartRateOrHrv"]);

        if (command.FatigueLevel is < 0 or > 100)
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidFatigueLevel"]);

        if (command.CortisolLevel < 0)
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidCortisolLevel"]);

        if (!ValidSensorStatuses.Contains(command.SensorStatus.ToUpperInvariant()))
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InvalidStatus,
                _localizer["InvalidSensorStatus"]);

        var vitalSignReading = new VitalSignReading(command);

        try
        {
            await vitalSignReadingRepository.AddAsync(vitalSignReading, cancellationToken);
            
            // Generate real-time anomaly, alert, and audit if thresholds are exceeded
            if (command.HeartRate > 100 || command.FatigueLevel > 80)
            {
                var anomalyCommand = new CreateVitalSignAnomalyCommand(
                    command.OrganizationId,
                    command.UserId,
                    "HEART_RATE_SPIKE",
                    "CRITICAL",
                    "OPEN",
                    command.HeartRate.ToString(),
                    "100",
                    "High heart rate or fatigue detected during vital sign reading.",
                    DateTimeOffset.UtcNow
                );
                await vitalSignAnomalyCommandService.Handle(anomalyCommand, cancellationToken);

                var alertCommand = new CreateClinicalAlertCommand(
                    command.OrganizationId,
                    command.UserId,
                    "CRITICAL",
                    "OPEN",
                    "Critical biometric reading detected, immediate review required.",
                    DateTimeOffset.UtcNow
                );
                await clinicalAlertCommandService.Handle(alertCommand, cancellationToken);
            }

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<VitalSignReading>.Success(vitalSignReading);
        }
        catch (OperationCanceledException)
        {
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["CreateVitalSignReadingDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<VitalSignReading>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedVitalSignReadingCreateError"]);
        }
    }
}