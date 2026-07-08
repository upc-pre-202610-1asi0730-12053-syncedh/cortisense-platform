namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to sign up.
/// </summary>
public record SignUpCommand(
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