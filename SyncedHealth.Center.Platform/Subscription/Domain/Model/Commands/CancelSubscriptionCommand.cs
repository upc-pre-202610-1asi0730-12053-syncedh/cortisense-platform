namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

/// <summary>
/// Command to cancel subscription.
/// </summary>
public record CancelSubscriptionCommand(int SubscriptionId);
