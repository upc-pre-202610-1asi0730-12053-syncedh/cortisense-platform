namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

/// <summary>
/// Command to create risk assessment.
/// </summary>
public record CreateRiskAssessmentCommand(
    int OrganizationId,
    int UserId,
    int FatigueLevel,
    string RiskLevel,
    int HeartRate,
    int Hrv,
    DateTimeOffset? LastUpdatedAt
);