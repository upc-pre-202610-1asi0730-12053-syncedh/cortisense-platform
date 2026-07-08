namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get risk assessments by user id in the CortiSense Platform.
/// </summary>
public record GetRiskAssessmentsByUserIdQuery(int UserId);