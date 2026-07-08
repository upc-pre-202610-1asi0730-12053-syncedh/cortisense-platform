using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

/// <summary>
/// Represents the risk assessment in the CortiSense Platform.
/// </summary>
public partial class RiskAssessment : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}