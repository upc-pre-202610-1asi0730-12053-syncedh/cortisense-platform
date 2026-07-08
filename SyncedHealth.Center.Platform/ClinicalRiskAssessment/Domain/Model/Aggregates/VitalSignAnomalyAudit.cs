using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

/// <summary>
/// Represents the vital sign anomaly in the CortiSense Platform.
/// </summary>
public partial class VitalSignAnomaly : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}