namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

public record IssueRecoveryRecommendationResource(
    int MedicalStaffId,
    string Description,
    int SuggestedRestDays
);