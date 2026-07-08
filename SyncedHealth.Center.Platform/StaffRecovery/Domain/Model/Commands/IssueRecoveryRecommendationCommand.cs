namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

/// <summary>
/// Command to issue recovery recommendation.
/// </summary>
public record IssueRecoveryRecommendationCommand(
    int MedicalStaffId,
    string Description,
    int SuggestedRestDays
);