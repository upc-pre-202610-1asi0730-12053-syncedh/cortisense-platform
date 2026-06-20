using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CheckoutSessionRepository(AppDbContext context) : BaseRepository<CheckoutSession>(context), ICheckoutSessionRepository
{
    public async Task<IEnumerable<CheckoutSession>> FindByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<CheckoutSession>()
            .Where(c => c.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }
}
