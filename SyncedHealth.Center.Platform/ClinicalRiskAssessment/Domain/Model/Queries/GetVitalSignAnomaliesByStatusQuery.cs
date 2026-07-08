namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get vital sign anomalies by status in the CortiSense Platform.
/// </summary>
public record GetVitalSignAnomaliesByStatusQuery(string Status);