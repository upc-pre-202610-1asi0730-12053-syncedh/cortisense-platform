namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

public record VitalSignAnomalyResource(
    int Id,
    int OrganizationId,
    int UserId,
    string Type,
    string Severity,
    string Status,
    string Value,
    string Threshold,
    string Message,
    DateTimeOffset? DetectedAt,
    DateTimeOffset? ReviewedAt,
    int? ReviewedBy
);