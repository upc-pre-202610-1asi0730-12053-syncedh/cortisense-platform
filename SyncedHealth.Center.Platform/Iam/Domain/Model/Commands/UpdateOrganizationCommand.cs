namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

public record UpdateOrganizationCommand(
    int Id,
    string? Name,
    string? Ruc,
    string? Email,
    string? Phone,
    string? Address,
    string? Status,
    string? RegistrationStatus
);