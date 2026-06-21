using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RecoveryPlanRepository(AppDbContext context)
    : BaseRepository<RecoveryPlan>(context), IRecoveryPlanRepository
{
    public async Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(
        int medicalStaffId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(recoveryPlan => recoveryPlan.MedicalStaffId == medicalStaffId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RecoveryPlan>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status.Trim().ToUpperInvariant();

        return await Context.Set<RecoveryPlan>()
            .Where(recoveryPlan => recoveryPlan.Status == normalizedStatus)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RecoveryPlan>> FindBySuggestedRestDaysAsync(
        int suggestedRestDays,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(recoveryPlan => recoveryPlan.SuggestedRestDays == suggestedRestDays)
            .ToListAsync(cancellationToken);
    }
}