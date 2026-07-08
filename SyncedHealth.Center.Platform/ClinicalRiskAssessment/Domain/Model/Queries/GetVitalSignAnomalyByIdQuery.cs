namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get vital sign anomaly by id in the CortiSense Platform.
/// </summary>
public record GetVitalSignAnomalyByIdQuery(int Id);