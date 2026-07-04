using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.Subscription.Application.CommandServices;

public interface ISubscriptionCommandService
{
    Task<Result<Domain.Model.Aggregates.Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Subscription>> Handle(UpdateSubscriptionCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Subscription>> Handle(CancelSubscriptionCommand command, CancellationToken cancellationToken);
}
