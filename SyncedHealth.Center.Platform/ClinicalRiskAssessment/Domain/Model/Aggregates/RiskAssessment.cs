using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

/// <summary>
/// Represents the risk assessment in the CortiSense Platform.
/// </summary>
public partial class RiskAssessment
{
    public RiskAssessment()
    {
        RiskLevel = string.Empty;
    }

    public RiskAssessment(CreateRiskAssessmentCommand command)
    {
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        FatigueLevel = command.FatigueLevel;
        RiskLevel = command.RiskLevel.ToUpperInvariant();
        HeartRate = command.HeartRate;
        Hrv = command.Hrv;
        LastUpdatedAt = command.LastUpdatedAt ?? DateTimeOffset.UtcNow;
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public int FatigueLevel { get; set; }

    public string RiskLevel { get; set; }

    public int HeartRate { get; set; }

    public int Hrv { get; set; }

    public DateTimeOffset? LastUpdatedAt { get; set; }

    public void UpdateRiskLevel(string riskLevel)
    {
        RiskLevel = riskLevel.ToUpperInvariant();
        LastUpdatedAt = DateTimeOffset.UtcNow;
    }
}