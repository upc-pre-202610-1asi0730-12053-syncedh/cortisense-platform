using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

public interface ICheckoutSessionRepository : IBaseRepository<CheckoutSession>
{
    Task<IEnumerable<CheckoutSession>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<CheckoutSession?> FindByStripeSessionIdAsync(
        string stripeSessionId,
        CancellationToken cancellationToken = default
    );
}