using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

public interface IClinicalAlertRepository : IBaseRepository<ClinicalAlert>
{
    Task<IEnumerable<ClinicalAlert>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ClinicalAlert>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ClinicalAlert>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default
    );
}