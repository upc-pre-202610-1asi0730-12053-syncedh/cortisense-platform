using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

/// <summary>
/// Represents the checkout session repository in the CortiSense Platform.
/// </summary>
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