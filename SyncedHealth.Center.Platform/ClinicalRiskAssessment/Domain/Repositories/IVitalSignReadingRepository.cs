using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

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