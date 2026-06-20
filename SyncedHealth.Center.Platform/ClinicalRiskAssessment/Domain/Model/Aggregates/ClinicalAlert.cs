using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;

public partial class ClinicalAlert
{
    public ClinicalAlert()
    {
        Severity = string.Empty;
        Status = string.Empty;
        Message = string.Empty;
    }

    public ClinicalAlert(CreateClinicalAlertCommand command)
    {
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        Severity = command.Severity.ToUpperInvariant();
        Status = command.Status.ToUpperInvariant();
        Message = command.Message;
        CreatedAt = command.CreatedAt ?? DateTimeOffset.UtcNow;
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public string Severity { get; set; }

    public string Status { get; set; }

    public string Message { get; set; }

    public DateTimeOffset? ResolvedAt { get; set; }

    public int? ResolvedBy { get; set; }

    public void UpdateStatus(string status, int? resolvedBy)
    {
        Status = status.ToUpperInvariant();

        if (Status == "RESOLVED")
        {
            ResolvedAt = DateTimeOffset.UtcNow;
            ResolvedBy = resolvedBy;
        }
    }
}