using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

/// <summary>
/// Represents the subscription in the CortiSense Platform.
/// </summary>
public partial class Subscription
{
    public Subscription()
    {
    }

    public Subscription(CreateSubscriptionCommand command)
    {
        OrganizationId = command.OrganizationId;
        PlanId = command.PlanId;
        Status = ESubscriptionStatus.Pending;
        StartedAt = command.StartedAt ?? DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }

    public int OrganizationId { get; private set; }

    public int PlanId { get; private set; }

    public ESubscriptionStatus Status { get; private set; }

    public DateTimeOffset StartedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public string? StripeSubscriptionId { get; private set; }

    public string? StripeCustomerId { get; private set; }

    public void UpdatePlan(int planId)
    {
        PlanId = planId;
    }

    public void Activate(string? stripeSubscriptionId, string? stripeCustomerId)
    {
        Status = ESubscriptionStatus.Active;
        StartedAt = DateTimeOffset.UtcNow;
        StripeSubscriptionId = stripeSubscriptionId;
        StripeCustomerId = stripeCustomerId;
    }

    public void Cancel()
    {
        Status = ESubscriptionStatus.Cancelled;
        CancelledAt = DateTimeOffset.UtcNow;
    }
}