using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

/// <summary>
/// Represents the plan in the CortiSense Platform.
/// </summary>
public partial class Plan : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
