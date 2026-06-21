using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;

public interface IRecoveryPlanCommandService
{
    Task<RecoveryPlan> Handle(IssueRecoveryRecommendationCommand command);
}