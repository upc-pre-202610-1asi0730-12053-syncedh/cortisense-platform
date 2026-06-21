namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record SignUpResource(
    int OrganizationId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role,
    string Status,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId,
    string? RegistrationStatus
);