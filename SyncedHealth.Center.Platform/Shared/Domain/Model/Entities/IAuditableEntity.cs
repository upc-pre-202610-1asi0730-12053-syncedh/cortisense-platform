namespace SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

/// <summary>
/// Represents the auditable entity in the CortiSense Platform.
/// </summary>
public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }

    DateTimeOffset? UpdatedAt { get; set; }
}