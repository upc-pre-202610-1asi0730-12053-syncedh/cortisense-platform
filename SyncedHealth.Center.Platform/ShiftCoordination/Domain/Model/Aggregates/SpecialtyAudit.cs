using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

/// <summary>
/// Represents the specialty in the CortiSense Platform.
/// </summary>
public partial class Specialty : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}