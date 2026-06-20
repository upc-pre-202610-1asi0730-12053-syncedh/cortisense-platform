using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;

public interface IVitalSignAnomalyQueryService
{
    Task<IEnumerable<VitalSignAnomaly>> Handle(GetAllVitalSignAnomaliesQuery query, CancellationToken cancellationToken);
    Task<VitalSignAnomaly?> Handle(GetVitalSignAnomalyByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<VitalSignAnomaly>> Handle(GetVitalSignAnomaliesByOrganizationIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<VitalSignAnomaly>> Handle(GetVitalSignAnomaliesByUserIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<VitalSignAnomaly>> Handle(GetVitalSignAnomaliesByStatusQuery query, CancellationToken cancellationToken);
}