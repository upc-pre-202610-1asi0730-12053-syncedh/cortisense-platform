using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Represents the team member repository in the CortiSense Platform.
/// </summary>
public class TeamMemberRepository(AppDbContext context)
    : BaseRepository<TeamMember>(context), ITeamMemberRepository
{
    public async Task<IEnumerable<TeamMember>> FindByTeamIdAsync(
        int teamId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TeamMember>()
            .Where(teamMember => teamMember.TeamId == teamId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TeamMember>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TeamMember>()
            .Where(teamMember => teamMember.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TeamMember>()
            .AnyAsync(teamMember => teamMember.UserId == userId, cancellationToken);
    }
}