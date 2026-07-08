using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

/// <summary>
/// Represents the vital sign anomaly repository in the CortiSense Platform.
/// </summary>
public interface IVitalSignAnomalyRepository : IBaseRepository<VitalSignAnomaly>
{
    Task<IEnumerable<VitalSignAnomaly>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<VitalSignAnomaly>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<VitalSignAnomaly>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default
    );
}