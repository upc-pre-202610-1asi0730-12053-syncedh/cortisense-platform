namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

public record CreateClinicalAlertCommand(
    int OrganizationId,
    int UserId,
    string Severity,
    string Status,
    string Message,
    DateTimeOffset? CreatedAt
);