using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

/// <summary>
/// Represents the checkout session in the CortiSense Platform.
/// </summary>
public partial class CheckoutSession
{
    public CheckoutSession()
    {
        PlanCode = string.Empty;
    }

    public CheckoutSession(CreateCheckoutSessionCommand command)
    {
        OrganizationId = command.OrganizationId;
        AdministratorId = command.AdministratorId;
        SubscriptionId = command.SubscriptionId;
        PlanId = command.PlanId;
        PlanCode = command.PlanCode;
        Status = ECheckoutSessionStatus.Pending;
    }

    public int Id { get; private set; }

    public int OrganizationId { get; private set; }

    public int AdministratorId { get; private set; }

    public int SubscriptionId { get; private set; }

    public int PlanId { get; private set; }

    public string PlanCode { get; private set; }

    public ECheckoutSessionStatus Status { get; private set; }

    public string? StripeSessionId { get; private set; }

    public string? StripeUrl { get; private set; }

    public string? StripeSubscriptionId { get; private set; }

    public string? StripeCustomerId { get; private set; }

    public DateTimeOffset? CompletedAt { get; private set; }

    public DateTimeOffset? FailedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public string? ErrorMessage { get; private set; }

    public void AttachStripeSession(string stripeSessionId, string stripeUrl)
    {
        StripeSessionId = stripeSessionId;
        StripeUrl = stripeUrl;
    }

    public void MarkAsCompleted(string? stripeSubscriptionId, string? stripeCustomerId)
    {
        Status = ECheckoutSessionStatus.Completed;
        CompletedAt = DateTimeOffset.UtcNow;
        StripeSubscriptionId = stripeSubscriptionId;
        StripeCustomerId = stripeCustomerId;
    }

    public void MarkAsFailed(string? errorMessage = null)
    {
        Status = ECheckoutSessionStatus.Failed;
        FailedAt = DateTimeOffset.UtcNow;
        ErrorMessage = errorMessage;
    }

    public void MarkAsCancelled()
    {
        Status = ECheckoutSessionStatus.Failed;
        CancelledAt = DateTimeOffset.UtcNow;
    }
}