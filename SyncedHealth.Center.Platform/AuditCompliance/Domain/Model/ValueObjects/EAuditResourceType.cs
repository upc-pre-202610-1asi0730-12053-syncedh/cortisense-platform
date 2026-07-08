namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

/// <summary>
/// Represents the e audit resource type in the CortiSense Platform.
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