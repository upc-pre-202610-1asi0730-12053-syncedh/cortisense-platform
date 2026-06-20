namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the bounded context or module that generated an audit event.
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