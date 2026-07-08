using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.QueryServices;

/// <summary>
/// Represents the subscription query service in the CortiSense Platform.
/// </summary>
public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.ListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetSubscriptionByOrganizationIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<Domain.Model.Aggregates.Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.FindByIdAsync(query.SubscriptionId, cancellationToken);
    }
}
