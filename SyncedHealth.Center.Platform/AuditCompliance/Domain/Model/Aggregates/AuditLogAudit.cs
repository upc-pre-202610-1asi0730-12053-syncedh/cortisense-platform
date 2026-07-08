using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;

/// <summary>
/// Represents the audit log in the CortiSense Platform.
/// </summary>
public partial class AuditLog : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}