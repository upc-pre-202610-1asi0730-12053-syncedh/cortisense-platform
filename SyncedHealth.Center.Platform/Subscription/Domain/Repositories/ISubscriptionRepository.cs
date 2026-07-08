using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

/// <summary>
/// Represents the subscription repository in the CortiSense Platform.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Model.Aggregates.Subscription>
{
    Task<IEnumerable<Model.Aggregates.Subscription>> FindByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default);
}
