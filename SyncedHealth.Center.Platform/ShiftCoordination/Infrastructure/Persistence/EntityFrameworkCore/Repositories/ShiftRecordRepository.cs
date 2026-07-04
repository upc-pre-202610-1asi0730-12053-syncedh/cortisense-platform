using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ShiftRecordRepository(AppDbContext context)
    : BaseRepository<ShiftRecord>(context), IShiftRecordRepository
{
    public async Task<IEnumerable<ShiftRecord>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ShiftRecord>()
            .Where(shiftRecord => shiftRecord.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ShiftRecord>()
            .Where(shiftRecord => shiftRecord.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> FindByWorkAreaIdAsync(
        int workAreaId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ShiftRecord>()
            .Where(shiftRecord => shiftRecord.WorkAreaId == workAreaId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status.ToUpperInvariant();

        return await Context.Set<ShiftRecord>()
            .Where(shiftRecord => shiftRecord.Status == normalizedStatus)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> FindAvailableByWorkAreaAsync(
        int workAreaId,
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ShiftRecord>()
            .Where(sr =>
                sr.WorkAreaId == workAreaId &&
                sr.OrganizationId == organizationId &&
                sr.Status == "SCHEDULED")
            .ToListAsync(cancellationToken);
    }
}