using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Subscription.Application.QueryServices;

public interface ISubscriptionQueryService
{
    Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetAllSubscriptionsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Model.Aggregates.Subscription>> Handle(GetSubscriptionByOrganizationIdQuery query, CancellationToken cancellationToken);
}
