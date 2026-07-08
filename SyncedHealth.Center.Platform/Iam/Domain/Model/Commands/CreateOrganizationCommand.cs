namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to create organization.
/// </summary>
public record CreateOrganizationCommand(
    string Name,
    string Ruc,
    string Email,
    string Phone,
    string Address,
    string? Status,
    string? RegistrationStatus
);