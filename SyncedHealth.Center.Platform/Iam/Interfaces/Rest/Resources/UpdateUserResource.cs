namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update user resource in the CortiSense Platform.
/// </summary>
public record UpdateUserResource(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId,
    string? Role,
    string? Status,
    string? RegistrationStatus
);