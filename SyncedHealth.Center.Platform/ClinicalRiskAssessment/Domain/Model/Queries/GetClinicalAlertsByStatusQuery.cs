namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get clinical alerts by status in the CortiSense Platform.
/// </summary>
public record GetClinicalAlertsByStatusQuery(string Status);