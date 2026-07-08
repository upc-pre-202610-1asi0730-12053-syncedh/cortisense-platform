namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create vital sign anomaly resource in the CortiSense Platform.
/// </summary>
public record CreateVitalSignAnomalyResource(
    int OrganizationId,
    int UserId,
    string Type,
    string Severity,
    string Status,
    string Value,
    string Threshold,
    string Message,
    DateTimeOffset? DetectedAt
);