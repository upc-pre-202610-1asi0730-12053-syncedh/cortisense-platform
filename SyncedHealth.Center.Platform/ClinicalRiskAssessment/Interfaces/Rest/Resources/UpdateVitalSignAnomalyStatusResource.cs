namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update vital sign anomaly status resource in the CortiSense Platform.
/// </summary>
public record UpdateVitalSignAnomalyStatusResource(
    string Status,
    int? ReviewedBy
);