using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

/// <summary>
/// Represents the shift record in the CortiSense Platform.
/// </summary>
public partial class ShiftRecord : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}