namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the e audit log type in the CortiSense Platform.
/// </summary>
public enum EAuditLogType
{
    UserInvited,
    UserRoleChanged,
    TeamCreated,
    TeamUpdated,
    TeamDeleted,
    AlertCreated,
    AlertResolved,
    AnomalyReviewed,
    PreventiveActionCreated,
    PreventiveActionAccepted,
    PreventiveActionCompleted,
    ShiftCheckIn,
    SubscriptionActivated,
    ReportGenerated,
    SystemAccessed,
    RiskAssessmentEvaluated,
    AnomalyCreated
}