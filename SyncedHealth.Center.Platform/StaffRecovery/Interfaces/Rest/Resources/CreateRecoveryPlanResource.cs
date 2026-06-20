namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

public record CreateRecoveryPlanResource(
    int MedicalStaffId, 
    string Description, 
    int SuggestedRestDays
    );