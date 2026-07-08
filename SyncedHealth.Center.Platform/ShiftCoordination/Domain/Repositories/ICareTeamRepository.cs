using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

/// <summary>
/// Represents the care team repository in the CortiSense Platform.
/// </summary>
public interface ICareTeamRepository : IBaseRepository<CareTeam>
{
    Task<IEnumerable<CareTeam>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<CareTeam>> FindBySupervisorIdAsync(
        int supervisorId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<CareTeam>> FindByWorkAreaIdAsync(
        int workAreaId,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsActiveBySupervisorIdAsync(
        int supervisorId,
        int? excludedTeamId = null,
        CancellationToken cancellationToken = default
    );
}