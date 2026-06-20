namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model;

/// <summary>
/// Represents the possible errors in the audit compliance bounded context.
/// </summary>
public enum AuditComplianceError
{
    None,
    AuditLogNotFound,
    InvalidAuditLogData,
    AuditLogCreationFailed,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}