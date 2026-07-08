using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Subscription.Application.QueryServices;

/// <summary>
/// Represents the subscription query service in the CortiSense Platform.
/// </summary>
public interface ISubscriptionQueryService
{
    Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetSubscriptionByOrganizationIdQuery query, CancellationToken cancellationToken);
    Task<Domain.Model.Aggregates.Subscription?> Handle(GetSubscriptionByIdQuery query, CancellationToken cancellationToken);
}
