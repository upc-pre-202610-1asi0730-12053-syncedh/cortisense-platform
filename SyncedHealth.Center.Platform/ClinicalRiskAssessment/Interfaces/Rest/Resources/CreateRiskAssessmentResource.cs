namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create risk assessment resource in the CortiSense Platform.
/// </summary>
public record CreateRiskAssessmentResource(
    int OrganizationId,
    int UserId,
    int FatigueLevel,
    string RiskLevel,
    int HeartRate,
    int Hrv,
    DateTimeOffset? LastUpdatedAt
);