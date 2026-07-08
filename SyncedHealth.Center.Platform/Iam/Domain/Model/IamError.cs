namespace SyncedHealth.Center.Platform.Iam.Domain.Model;

/// <summary>
/// Represents the iam error in the CortiSense Platform.
/// </summary>
public enum IamError
{
    None,
    UserNotFound,
    UsernameAlreadyTaken,
    InvalidCredentials,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    ExternalServiceError
}