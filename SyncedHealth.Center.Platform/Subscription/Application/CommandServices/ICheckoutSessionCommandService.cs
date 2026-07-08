using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.Subscription.Application.CommandServices;

/// <summary>
/// Represents the checkout session command service in the CortiSense Platform.
/// </summary>
public interface ICheckoutSessionCommandService
{
    Task<Result<CheckoutSession>> Handle(CreateCheckoutSessionCommand command, CancellationToken cancellationToken);
}
