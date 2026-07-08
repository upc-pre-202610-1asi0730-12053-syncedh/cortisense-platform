using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;

/// <summary>
/// Represents the recovery plan command service in the CortiSense Platform.
/// </summary>
public interface IRecoveryPlanCommandService
{
    Task<Result<RecoveryPlan>> Handle(
        IssueRecoveryRecommendationCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<RecoveryPlan>> Handle(
        AcceptRecoveryPlanCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<RecoveryPlan>> Handle(
        RejectRecoveryPlanCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<RecoveryPlan>> Handle(
        ConfirmRecoveryCommand command,
        CancellationToken cancellationToken = default);
}