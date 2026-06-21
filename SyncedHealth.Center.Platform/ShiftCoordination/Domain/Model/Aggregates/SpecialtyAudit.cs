using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

public partial class Specialty : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}