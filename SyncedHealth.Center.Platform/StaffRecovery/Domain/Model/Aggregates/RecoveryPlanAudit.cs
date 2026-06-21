
using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

public partial class RecoveryPlan : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}