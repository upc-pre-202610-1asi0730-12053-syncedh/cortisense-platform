namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create organization resource in the CortiSense Platform.
/// </summary>
public record CreateOrganizationResource(
    string Name,
    string Ruc,
    string Email,
    string Phone,
    string Address,
    string? Status,
    string? RegistrationStatus
);