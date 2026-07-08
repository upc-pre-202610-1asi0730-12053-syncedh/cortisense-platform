namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the risk assessment resource in the CortiSense Platform.
/// </summary>
public record RiskAssessmentResource(
    int Id,
    int OrganizationId,
    int UserId,
    int FatigueLevel,
    string RiskLevel,
    int HeartRate,
    int Hrv,
    DateTimeOffset? LastUpdatedAt
);