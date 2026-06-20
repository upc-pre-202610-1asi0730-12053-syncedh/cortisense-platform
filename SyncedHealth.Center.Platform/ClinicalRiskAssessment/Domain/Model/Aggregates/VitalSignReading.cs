using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

public partial class VitalSignReading
{
    public VitalSignReading()
    {
        SensorStatus = string.Empty;
    }

    public VitalSignReading(CreateVitalSignReadingCommand command)
    {
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        HeartRate = command.HeartRate;
        Hrv = command.Hrv;
        FatigueLevel = command.FatigueLevel;
        CortisolLevel = command.CortisolLevel;
        SensorStatus = command.SensorStatus.ToUpperInvariant();
        RecordedAt = command.RecordedAt ?? DateTimeOffset.UtcNow;
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public int HeartRate { get; set; }

    public int Hrv { get; set; }

    public int FatigueLevel { get; set; }

    public decimal CortisolLevel { get; set; }

    public string SensorStatus { get; set; }

    public DateTimeOffset? RecordedAt { get; set; }
}