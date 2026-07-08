using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Represents the plan repository in the CortiSense Platform.
/// </summary>
public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
}
