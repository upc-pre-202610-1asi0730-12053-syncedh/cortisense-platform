namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the authenticated user resource in the CortiSense Platform.
/// </summary>
public record AuthenticatedUserResource(
    int Id,
    int OrganizationId,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Status,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId,
    string RegistrationStatus,
    string Token
);