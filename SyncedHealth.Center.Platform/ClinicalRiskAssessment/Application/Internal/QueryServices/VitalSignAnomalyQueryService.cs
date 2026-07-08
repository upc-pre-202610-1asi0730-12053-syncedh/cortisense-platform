using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.Internal.QueryServices;

/// <summary>
/// Represents the vital sign anomaly query service in the CortiSense Platform.
/// </summary>
public class VitalSignAnomalyQueryService(IVitalSignAnomalyRepository vitalSignAnomalyRepository)
    : IVitalSignAnomalyQueryService
{
    public async Task<IEnumerable<VitalSignAnomaly>> Handle(
        GetAllVitalSignAnomaliesQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignAnomalyRepository.ListAsync(cancellationToken);
    }

    public async Task<VitalSignAnomaly?> Handle(
        GetVitalSignAnomalyByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignAnomalyRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<VitalSignAnomaly>> Handle(
        GetVitalSignAnomaliesByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignAnomalyRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<IEnumerable<VitalSignAnomaly>> Handle(
        GetVitalSignAnomaliesByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignAnomalyRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    public async Task<IEnumerable<VitalSignAnomaly>> Handle(
        GetVitalSignAnomaliesByStatusQuery query,
        CancellationToken cancellationToken)
    {
        return await vitalSignAnomalyRepository.FindByStatusAsync(query.Status, cancellationToken);
    }
}