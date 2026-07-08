namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update clinical alert status resource in the CortiSense Platform.
/// </summary>
public record UpdateClinicalAlertStatusResource(
    string Status,
    int? ResolvedBy
);