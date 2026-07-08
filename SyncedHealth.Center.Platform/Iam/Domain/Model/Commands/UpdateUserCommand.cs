namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to update user.
/// </summary>
public record UpdateUserCommand(
    int Id,
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