
using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

/// <summary>
/// Represents the recovery plan in the CortiSense Platform.
/// </summary>
public partial class RecoveryPlan : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}