using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SubscriptionRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Subscription>(context), ISubscriptionRepository
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Subscription>> FindByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Domain.Model.Aggregates.Subscription>()
            .Where(s => s.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }
}
