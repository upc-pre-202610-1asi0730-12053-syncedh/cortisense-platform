using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Subscription.Application.QueryServices;

public interface ICheckoutSessionQueryService
{
    Task<IEnumerable<CheckoutSession>> Handle(GetCheckoutSessionsByOrganizationIdQuery query, CancellationToken cancellationToken);
}
