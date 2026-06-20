using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.Internal.CommandServices;

public class RecoveryPlanCommandService : IRecoveryPlanCommandService
{
    private readonly IRecoveryPlanRepository _recoveryPlanRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public RecoveryPlanCommandService(IRecoveryPlanRepository recoveryPlanRepository, IUnitOfWork unitOfWork)
    {
        _recoveryPlanRepository = recoveryPlanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RecoveryPlan> Handle(IssueRecoveryRecommendationCommand command)
    {
        var recoveryPlan = new RecoveryPlan(command);
        
        await _recoveryPlanRepository.AddAsync(recoveryPlan);
        
        await _unitOfWork.CompleteAsync();
        
        return recoveryPlan;
    }
}