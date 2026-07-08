using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

/// <summary>
/// Represents the subscription in the CortiSense Platform.
/// </summary>
public partial class Subscription : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
