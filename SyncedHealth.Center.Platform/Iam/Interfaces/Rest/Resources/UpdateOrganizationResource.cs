namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update organization resource in the CortiSense Platform.
/// </summary>
public record UpdateOrganizationResource(
    string? Name,
    string? Ruc,
    string? Email,
    string? Phone,
    string? Address,
    string? Status,
    string? RegistrationStatus
);