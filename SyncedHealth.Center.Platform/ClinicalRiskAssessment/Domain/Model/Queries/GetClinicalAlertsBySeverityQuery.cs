namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get clinical alerts by severity in the CortiSense Platform.
/// </summary>
public record GetClinicalAlertsBySeverityQuery(string Severity);
