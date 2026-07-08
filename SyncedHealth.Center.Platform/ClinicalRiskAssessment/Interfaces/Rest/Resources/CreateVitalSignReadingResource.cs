namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create vital sign reading resource in the CortiSense Platform.
/// </summary>
public record CreateVitalSignReadingResource(
    int OrganizationId,
    int UserId,
    int HeartRate,
    int Hrv,
    int FatigueLevel,
    decimal CortisolLevel,
    string SensorStatus,
    DateTimeOffset? RecordedAt
);