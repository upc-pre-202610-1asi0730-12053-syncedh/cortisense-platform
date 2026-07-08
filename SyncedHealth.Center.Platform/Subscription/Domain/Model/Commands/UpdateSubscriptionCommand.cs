namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

/// <summary>
/// Command to update subscription.
/// </summary>
public record UpdateSubscriptionCommand(int SubscriptionId, int PlanId);
