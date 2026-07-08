using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;

/// <summary>
/// Represents the risk assessment repository in the CortiSense Platform.
/// </summary>
public interface IRiskAssessmentRepository : IBaseRepository<RiskAssessment>
{
    Task<IEnumerable<RiskAssessment>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<RiskAssessment>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );
}