namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

public record UpdateSubscriptionCommand(int SubscriptionId, int PlanId);
