namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

/// <summary>
/// Represents the issue recovery recommendation resource in the CortiSense Platform.
/// </summary>
public record IssueRecoveryRecommendationResource(
    int MedicalStaffId,
    string Description,
    int SuggestedRestDays
);