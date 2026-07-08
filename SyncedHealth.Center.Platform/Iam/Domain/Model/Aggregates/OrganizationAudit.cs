using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
/// Represents the organization in the CortiSense Platform.
/// </summary>
public partial class Organization : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}