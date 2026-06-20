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

public class VitalSignReadingCommandService(
    IVitalSignReadingRepository vitalSignReadingRepository,
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