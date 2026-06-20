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

public class VitalSignAnomalyCommandService(
    IVitalSignAnomalyRepository vitalSignAnomalyRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ClinicalRiskAssessmentMessages> localizer)
    : IVitalSignAnomalyCommandService
{
    private readonly IStringLocalizer<ClinicalRiskAssessmentMessages> _localizer = localizer;

    private static readonly string[] ValidSeverities = ["LOW", "MEDIUM", "HIGH", "CRITICAL"];
    private static readonly string[] ValidStatuses = ["OPEN", "REVIEWED", "RESOLVED"];

    public async Task<Result<VitalSignAnomaly>> Handle(
        CreateVitalSignAnomalyCommand command,
        CancellationToken cancellationToken)
    {
        if (command.OrganizationId <= 0 || command.UserId <= 0)
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["InvalidOrganizationOrUser"]);

        if (string.IsNullOrWhiteSpace(command.Type))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["TypeRequired"]);

        if (!ValidSeverities.Contains(command.Severity.ToUpperInvariant()))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidSeverity,
                _localizer["InvalidSeverity"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidStatus,
                _localizer["InvalidAnomalyStatus"]);

        if (string.IsNullOrWhiteSpace(command.Value) || string.IsNullOrWhiteSpace(command.Threshold))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["ValueAndThresholdRequired"]);

        if (string.IsNullOrWhiteSpace(command.Message))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidBiometricData,
                _localizer["MessageRequired"]);

        var vitalSignAnomaly = new VitalSignAnomaly(command);

        try
        {
            await vitalSignAnomalyRepository.AddAsync(vitalSignAnomaly, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<VitalSignAnomaly>.Success(vitalSignAnomaly);
        }
        catch (OperationCanceledException)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["CreateVitalSignAnomalyDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedVitalSignAnomalyCreateError"]);
        }
    }

    public async Task<Result<VitalSignAnomaly>> Handle(
        UpdateVitalSignAnomalyStatusCommand command,
        CancellationToken cancellationToken)
    {
        var vitalSignAnomaly = await vitalSignAnomalyRepository.FindByIdAsync(command.Id, cancellationToken);

        if (vitalSignAnomaly is null)
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.VitalSignAnomalyNotFound,
                _localizer["VitalSignAnomalyNotFound"]);

        if (!ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InvalidStatus,
                _localizer["InvalidAnomalyStatus"]);

        vitalSignAnomaly.UpdateStatus(command.Status, command.ReviewedBy);

        try
        {
            vitalSignAnomalyRepository.Update(vitalSignAnomaly);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<VitalSignAnomaly>.Success(vitalSignAnomaly);
        }
        catch (OperationCanceledException)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.OperationCancelled,
                _localizer["OperationCancelled"]);
        }
        catch (DbUpdateException)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.DatabaseError,
                _localizer["UpdateVitalSignAnomalyDatabaseError"]);
        }
        catch (Exception)
        {
            return Result<VitalSignAnomaly>.Failure(
                ClinicalRiskAssessmentError.InternalServerError,
                _localizer["UnexpectedVitalSignAnomalyUpdateError"]);
        }
    }
}