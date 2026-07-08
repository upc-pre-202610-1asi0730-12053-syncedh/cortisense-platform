namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the vital sign reading resource in the CortiSense Platform.
/// </summary>
public record VitalSignReadingResource(
    int Id,
    int OrganizationId,
    int UserId,
    int HeartRate,
    int Hrv,
    int FatigueLevel,
    decimal CortisolLevel,
    string SensorStatus,
    DateTimeOffset? RecordedAt
);