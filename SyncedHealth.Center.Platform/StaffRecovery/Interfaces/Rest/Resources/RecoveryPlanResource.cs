namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;

/// <summary>
/// Represents the recovery plan resource in the CortiSense Platform.
/// </summary>
public record RecoveryPlanResource(
    int Id,
    int MedicalStaffId,
    string Description,
    int SuggestedRestDays,
    string Status,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);