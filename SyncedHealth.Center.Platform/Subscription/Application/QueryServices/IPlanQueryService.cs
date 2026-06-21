using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Subscription.Application.QueryServices;

public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken);
    Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken);
}
