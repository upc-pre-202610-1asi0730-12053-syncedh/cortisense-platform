namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create invitation resource in the CortiSense Platform.
/// </summary>
public record CreateInvitationResource(
    int OrganizationId,
    string Email,
    string Role,
    string? Status,
    string? Token,
    DateTimeOffset? ExpiresAt
);