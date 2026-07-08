namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

/// <summary>
/// Command to update vital sign anomaly status.
/// </summary>
public record UpdateVitalSignAnomalyStatusCommand(
    int Id,
    string Status,
    int? ReviewedBy
);