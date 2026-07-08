namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Errors;

/// <summary>
/// Represents the audit compliance error in the CortiSense Platform.
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