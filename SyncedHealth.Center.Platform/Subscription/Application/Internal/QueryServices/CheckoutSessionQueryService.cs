using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.QueryServices;

public class CheckoutSessionQueryService(ICheckoutSessionRepository checkoutSessionRepository) : ICheckoutSessionQueryService
{
    public async Task<IEnumerable<CheckoutSession>> Handle(GetCheckoutSessionsByOrganizationIdQuery query, CancellationToken cancellationToken)
    {
        return await checkoutSessionRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }
}
