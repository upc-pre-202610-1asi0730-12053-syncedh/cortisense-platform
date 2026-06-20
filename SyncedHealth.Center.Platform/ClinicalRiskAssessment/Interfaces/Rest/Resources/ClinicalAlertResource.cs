namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

public record ClinicalAlertResource(
    int Id,
    int OrganizationId,
    int UserId,
    string Severity,
    string Status,
    string Message,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? ResolvedAt,
    int? ResolvedBy
);