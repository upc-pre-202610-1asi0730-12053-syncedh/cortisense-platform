namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the type of action recorded in an audit log.
/// </summary>
public enum EAuditLogType
{
    UserInvited,
    UserRoleChanged,
    TeamCreated,
    TeamUpdated,
    AlertResolved,
    AnomalyReviewed,
    PreventiveActionCreated,
    PreventiveActionCompleted,
    ShiftCheckIn,
    SubscriptionActivated,
    ReportGenerated,
    SystemAccessed
}