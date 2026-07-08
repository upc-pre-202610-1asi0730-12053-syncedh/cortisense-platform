namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

/// <summary>
/// Command to create clinical alert.
/// </summary>
public record CreateClinicalAlertCommand(
    int OrganizationId,
    int UserId,
    string Severity,
    string Status,
    string Message,
    DateTimeOffset? CreatedAt
);