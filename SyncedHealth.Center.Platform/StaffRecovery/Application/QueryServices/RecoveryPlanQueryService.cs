using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;

public class RecoveryPlanQueryService(IRecoveryPlanRepository recoveryPlanRepository) : IRecoveryPlanQueryService
{
    public async Task<IEnumerable<RecoveryPlan>> Handle(GetAllRecoveryPlansQuery query)
    {
        return await recoveryPlanRepository.ListAsync();
    }

    public async Task<RecoveryPlan?> Handle(GetRecoveryPlanByIdQuery query)
    {
        return await recoveryPlanRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansByMedicalStaffIdQuery query)
    {
        // Nota: Asegúrate de que este método exista en tu IRecoveryPlanRepository
        return await recoveryPlanRepository.FindByMedicalStaffIdAsync(query.MedicalStaffId);
    }

    public async Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansByStatusQuery query)
    {
        // Nota: Asegúrate de que este método exista en tu IRecoveryPlanRepository
        return await recoveryPlanRepository.FindByStatusAsync(query.Status);
    }

    public async Task<IEnumerable<RecoveryPlan>> Handle(GetRecoveryPlansBySuggestedRestDaysQuery query)
    {
        // Nota: Asegúrate de que este método exista en tu IRecoveryPlanRepository
        return await recoveryPlanRepository.FindBySuggestedRestDaysAsync(query.SuggestedRestDays);
    }
}