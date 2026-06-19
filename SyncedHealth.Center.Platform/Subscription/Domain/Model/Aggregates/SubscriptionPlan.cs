namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public class SubscriptionPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int MaxUsers { get; set; }
}
