namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create clinical alert resource in the CortiSense Platform.
/// </summary>
public record CreateClinicalAlertResource(
    int OrganizationId,
    int UserId,
    string Severity,
    string Status,
    string Message,
    DateTimeOffset? CreatedAt
);