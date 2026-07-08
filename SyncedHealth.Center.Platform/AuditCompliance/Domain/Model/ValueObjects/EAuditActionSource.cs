namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the e audit action source in the CortiSense Platform.
/// </summary>
public enum EAuditActionSource
{
    System,
    Iam,
    ClinicalRiskAssessment,
    StaffRecovery,
    ShiftCoordination,
    SubscriptionPlanManagement,
    AuditCompliance
}