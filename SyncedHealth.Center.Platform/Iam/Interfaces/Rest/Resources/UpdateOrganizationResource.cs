namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record UpdateOrganizationResource(
    string? Name,
    string? Ruc,
    string? Email,
    string? Phone,
    string? Address,
    string? Status,
    string? RegistrationStatus
);