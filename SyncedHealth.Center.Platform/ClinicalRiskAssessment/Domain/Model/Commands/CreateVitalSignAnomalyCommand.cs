namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

public record CreateVitalSignAnomalyCommand(
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