using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;

/// <summary>
/// Represents the recovery plan query service in the CortiSense Platform.
/// </summary>
public interface IRecoveryPlanQueryService
{
    Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetAllRecoveryPlansQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<RecoveryPlan>> Handle(
        GetRecoveryPlanByIdQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansByMedicalStaffIdQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansByStatusQuery query,
        CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<RecoveryPlan>>> Handle(
        GetRecoveryPlansBySuggestedRestDaysQuery query,
        CancellationToken cancellationToken = default);
}