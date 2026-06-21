using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

public interface ITeamMemberRepository : IBaseRepository<TeamMember>
{
    Task<IEnumerable<TeamMember>> FindByTeamIdAsync(
        int teamId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TeamMember>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );
}