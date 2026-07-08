namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

/// <summary>
/// Command to create vital sign reading.
/// </summary>
public record CreateVitalSignReadingCommand(
    int OrganizationId,
    int UserId,
    int HeartRate,
    int Hrv,
    int FatigueLevel,
    decimal CortisolLevel,
    string SensorStatus,
    DateTimeOffset? RecordedAt
);