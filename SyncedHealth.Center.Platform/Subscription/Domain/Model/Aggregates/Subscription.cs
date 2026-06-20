using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public partial class Subscription
{
    public Subscription()
    {
    }

    public Subscription(CreateSubscriptionCommand command)
    {
        OrganizationId = command.OrganizationId;
        PlanId = command.PlanId;
        Status = ESubscriptionStatus.Active;
        StartedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int OrganizationId { get; private set; }
    public int PlanId { get; private set; }
    public ESubscriptionStatus Status { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }

    public void UpdatePlan(int planId)
    {
        PlanId = planId;
    }

    public void Cancel()
    {
        Status = ESubscriptionStatus.Cancelled;
    }
}
