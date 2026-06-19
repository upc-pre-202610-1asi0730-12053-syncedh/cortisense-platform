using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public SubscriptionPlan? Plan { get; set; }
    public ESubscriptionStatus Status { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}