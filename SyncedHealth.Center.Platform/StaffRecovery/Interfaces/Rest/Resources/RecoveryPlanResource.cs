namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

public record RecoveryPlanResource(
    int Id,
    int MedicalStaffId,
    string Description,
    int SuggestedRestDays,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);