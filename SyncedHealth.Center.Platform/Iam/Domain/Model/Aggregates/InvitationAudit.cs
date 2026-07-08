using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
/// Represents the invitation in the CortiSense Platform.
/// </summary>
public partial class Invitation : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}