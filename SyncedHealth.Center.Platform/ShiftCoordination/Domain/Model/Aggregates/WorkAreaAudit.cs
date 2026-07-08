using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

/// <summary>
/// Represents the work area in the CortiSense Platform.
/// </summary>
public partial class WorkArea : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}