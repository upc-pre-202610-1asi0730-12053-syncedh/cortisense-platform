namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

public record IssueRecoveryRecommendationCommand(
    int MedicalStaffId, 
    string Description, 
    int SuggestedRestDays);