using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

public interface IRecoveryPlanRepository : IBaseRepository<RecoveryPlan> // O la clase base que uses
{
    Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(int medicalStaffId);
    Task<IEnumerable<RecoveryPlan>> FindByStatusAsync(string status);
    Task<IEnumerable<RecoveryPlan>> FindBySuggestedRestDaysAsync(int suggestedRestDays);
}