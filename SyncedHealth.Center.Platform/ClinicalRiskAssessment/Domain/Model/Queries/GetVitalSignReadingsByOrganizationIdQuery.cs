namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;

/// <summary>
/// Represents a query to get vital sign readings by organization id in the CortiSense Platform.
/// </summary>
public record GetVitalSignReadingsByOrganizationIdQuery(int OrganizationId);