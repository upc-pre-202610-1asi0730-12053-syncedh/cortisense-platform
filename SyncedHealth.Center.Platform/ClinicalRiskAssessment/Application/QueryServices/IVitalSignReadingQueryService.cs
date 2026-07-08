using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;

/// <summary>
/// Represents the vital sign reading query service in the CortiSense Platform.
/// </summary>
public interface IVitalSignReadingQueryService
{
    Task<IEnumerable<VitalSignReading>> Handle(GetAllVitalSignReadingsQuery query, CancellationToken cancellationToken);
    Task<VitalSignReading?> Handle(GetVitalSignReadingByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<VitalSignReading>> Handle(GetVitalSignReadingsByOrganizationIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<VitalSignReading>> Handle(GetVitalSignReadingsByUserIdQuery query, CancellationToken cancellationToken);
}