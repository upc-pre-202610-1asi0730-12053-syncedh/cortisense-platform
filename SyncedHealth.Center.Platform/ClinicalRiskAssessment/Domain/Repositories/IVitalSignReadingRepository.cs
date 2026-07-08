using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

/// <summary>
/// Represents the vital sign reading repository in the CortiSense Platform.
/// </summary>
public interface IVitalSignReadingRepository : IBaseRepository<VitalSignReading>
{
    Task<IEnumerable<VitalSignReading>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<VitalSignReading>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );
}