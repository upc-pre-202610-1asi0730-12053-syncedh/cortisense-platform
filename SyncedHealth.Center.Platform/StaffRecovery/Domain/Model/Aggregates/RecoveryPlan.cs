using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

/// <summary>
/// Represents the recovery plan in the CortiSense Platform.
/// </summary>
public partial class RecoveryPlan
{
    public RecoveryPlan()
    {
        Description = string.Empty;
        Status = "PENDING";
    }

    public RecoveryPlan(IssueRecoveryRecommendationCommand command)
    {
        MedicalStaffId = command.MedicalStaffId;
        Description = command.Description.Trim();
        SuggestedRestDays = command.SuggestedRestDays;
        Status = "PENDING";
    }

    public int Id { get; set; }

    public int MedicalStaffId { get; set; }

    public string Description { get; set; }

    public int SuggestedRestDays { get; set; }

    public string Status { get; set; }

    public void UpdateStatus(string status)
    {
        Status = status.Trim().ToUpperInvariant();
    }
}