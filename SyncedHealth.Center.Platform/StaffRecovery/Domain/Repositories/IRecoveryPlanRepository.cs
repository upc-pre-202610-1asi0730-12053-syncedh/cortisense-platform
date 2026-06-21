using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

public interface IRecoveryPlanRepository : IBaseRepository<RecoveryPlan>
{
    Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(int medicalStaffId);
}