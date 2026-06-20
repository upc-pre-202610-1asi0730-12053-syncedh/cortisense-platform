using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.QueryServices;

public class PlanQueryService(IPlanRepository planRepository) : IPlanQueryService
{
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.ListAsync(cancellationToken);
    }

    public async Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.FindByIdAsync(query.PlanId, cancellationToken);
    }
}
