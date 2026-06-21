using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RecoveryPlanRepository(AppDbContext context) : BaseRepository<RecoveryPlan>(context), IRecoveryPlanRepository
{
    public async Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(int medicalStaffId)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(p => p.MedicalStaffId == medicalStaffId)
            .ToListAsync();
    }

    public async Task<IEnumerable<RecoveryPlan>> FindByStatusAsync(string status)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<RecoveryPlan>> FindBySuggestedRestDaysAsync(int suggestedRestDays)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(p => p.SuggestedRestDays == suggestedRestDays)
            .ToListAsync();
    }
}