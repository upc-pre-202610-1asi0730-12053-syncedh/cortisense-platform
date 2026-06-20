using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

public partial class VitalSignAnomaly
{
    public VitalSignAnomaly()
    {
        Type = string.Empty;
        Severity = string.Empty;
        Status = string.Empty;
        Value = string.Empty;
        Threshold = string.Empty;
        Message = string.Empty;
    }

    public VitalSignAnomaly(CreateVitalSignAnomalyCommand command)
    {
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        Type = command.Type;
        Severity = command.Severity.ToUpperInvariant();
        Status = command.Status.ToUpperInvariant();
        Value = command.Value;
        Threshold = command.Threshold;
        Message = command.Message;
        DetectedAt = command.DetectedAt ?? DateTimeOffset.UtcNow;
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public string Type { get; set; }

    public string Severity { get; set; }

    public string Status { get; set; }

    public string Value { get; set; }

    public string Threshold { get; set; }

    public string Message { get; set; }

    public DateTimeOffset? DetectedAt { get; set; }

    public DateTimeOffset? ReviewedAt { get; set; }

    public int? ReviewedBy { get; set; }

    public void UpdateStatus(string status, int? reviewedBy)
    {
        Status = status.ToUpperInvariant();

        if (Status is "REVIEWED" or "RESOLVED")
        {
            ReviewedAt = DateTimeOffset.UtcNow;
            ReviewedBy = reviewedBy;
        }
    }
}