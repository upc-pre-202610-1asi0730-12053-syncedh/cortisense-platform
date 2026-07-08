using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Subscription.Application.QueryServices;

/// <summary>
/// Represents the checkout session query service in the CortiSense Platform.
/// </summary>
public interface ICheckoutSessionQueryService
{
    Task<IEnumerable<CheckoutSession>> Handle(GetCheckoutSessionsByOrganizationIdQuery query, CancellationToken cancellationToken);
}
