using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RecoveryPlanRepository : BaseRepository<RecoveryPlan>, IRecoveryPlanRepository
{
    public RecoveryPlanRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(int medicalStaffId)
    {
        return await Context.Set<RecoveryPlan>()
            .Where(rp => rp.MedicalStaffId == medicalStaffId)
            .ToListAsync();
    }
}