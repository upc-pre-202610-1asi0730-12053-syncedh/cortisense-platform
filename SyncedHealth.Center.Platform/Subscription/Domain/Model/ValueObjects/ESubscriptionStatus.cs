namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

/// <summary>
/// Represents the e subscription status in the CortiSense Platform.
/// </summary>
public enum ESubscriptionStatus
{
    Pending,
    Active,
    PastDue,
    Cancelled
}