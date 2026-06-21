namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

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