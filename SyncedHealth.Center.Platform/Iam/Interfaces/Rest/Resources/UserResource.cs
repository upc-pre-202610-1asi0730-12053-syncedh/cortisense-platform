namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the user resource in the CortiSense Platform.
/// </summary>
public record UserResource(
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
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);