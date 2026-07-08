namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the send invitation resource in the CortiSense Platform.
/// </summary>
public record SendInvitationResource(
    int OrganizationId,
    string Email,
    string Role,
    DateTimeOffset? ExpiresAt
);