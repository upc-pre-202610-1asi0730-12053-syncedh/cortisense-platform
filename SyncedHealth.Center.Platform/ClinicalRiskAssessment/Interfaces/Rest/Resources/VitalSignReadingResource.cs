namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;

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