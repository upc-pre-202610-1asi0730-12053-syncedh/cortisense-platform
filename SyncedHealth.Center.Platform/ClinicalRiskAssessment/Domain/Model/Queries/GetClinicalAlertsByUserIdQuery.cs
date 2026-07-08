namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get clinical alerts by user id in the CortiSense Platform.
/// </summary>
public record GetClinicalAlertsByUserIdQuery(int UserId);