using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;

public interface IRecoveryPlanQueryService
{
    Task<IEnumerable<RecoveryPlan>> Handle(GetAllRecoveryPlansQuery query);
    Task<RecoveryPlan?> Handle(GetRecoveryPlanByIdQuery query);
    Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansByMedicalStaffIdQuery query);
    Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansByStatusQuery query);
    Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansBySuggestedRestDaysQuery query);
}