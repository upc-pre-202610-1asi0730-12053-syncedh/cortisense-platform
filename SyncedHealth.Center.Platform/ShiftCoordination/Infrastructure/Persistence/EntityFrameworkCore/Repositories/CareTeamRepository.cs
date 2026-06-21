using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CareTeamRepository(AppDbContext context)
    : BaseRepository<CareTeam>(context), ICareTeamRepository
{
    public async Task<IEnumerable<CareTeam>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<CareTeam>()
            .Where(careTeam => careTeam.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CareTeam>> FindBySupervisorIdAsync(
        int supervisorId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<CareTeam>()
            .Where(careTeam => careTeam.SupervisorId == supervisorId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CareTeam>> FindByWorkAreaIdAsync(
        int workAreaId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<CareTeam>()
            .Where(careTeam => careTeam.WorkAreaId == workAreaId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsActiveBySupervisorIdAsync(
        int supervisorId,
        int? excludedTeamId = null,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Set<CareTeam>()
            .Where(careTeam =>
                careTeam.SupervisorId == supervisorId &&
                careTeam.Status == "ACTIVE");

        if (excludedTeamId.HasValue)
            query = query.Where(careTeam => careTeam.Id != excludedTeamId.Value);

        return await query.AnyAsync(cancellationToken);
    }
}