namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get vital sign anomalies by user id in the CortiSense Platform.
/// </summary>
public record GetVitalSignAnomaliesByUserIdQuery(int UserId);