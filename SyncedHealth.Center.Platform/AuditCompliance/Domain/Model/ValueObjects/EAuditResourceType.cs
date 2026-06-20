namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the system resource affected by an audited action.
/// </summary>
public enum EAuditResourceType
{
    User,
    Organization,
    CareTeam,
    TeamMember,
    ClinicalAlert,
    VitalSignAnomaly,
    PreventiveAction,
    Shift,
    Subscription,
    Report,
    System
}