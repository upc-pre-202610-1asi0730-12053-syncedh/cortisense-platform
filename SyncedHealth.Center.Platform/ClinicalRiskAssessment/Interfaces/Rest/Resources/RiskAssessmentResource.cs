namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

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