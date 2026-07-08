using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

/// <summary>
/// Represents the checkout session in the CortiSense Platform.
/// </summary>
public partial class CheckoutSession : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
