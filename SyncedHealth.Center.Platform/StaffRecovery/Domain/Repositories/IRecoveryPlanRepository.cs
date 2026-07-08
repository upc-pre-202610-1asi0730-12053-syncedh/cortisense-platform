using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

/// <summary>
/// Represents the recovery plan repository in the CortiSense Platform.
/// </summary>
public interface IRecoveryPlanRepository : IBaseRepository<RecoveryPlan>
{
    Task<IEnumerable<RecoveryPlan>> FindByMedicalStaffIdAsync(
        int medicalStaffId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<RecoveryPlan>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<RecoveryPlan>> FindBySuggestedRestDaysAsync(
        int suggestedRestDays,
        CancellationToken cancellationToken = default);
}