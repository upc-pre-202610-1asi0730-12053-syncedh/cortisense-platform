using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;

/// <summary>
/// Audit metadata for the audit log aggregate.
/// </summary>
/// <remarks>
/// CreatedAt and UpdatedAt are managed by the persistence layer through the auditable entity interceptor.
/// </remarks>
public partial class AuditLog : IAuditableEntity
{
    /// <summary>
    /// Gets or sets the UTC timestamp when the audit log was first persisted.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the audit log was last updated.
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
}