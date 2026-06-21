namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record SendInvitationResource(
    int OrganizationId,
    string Email,
    string Role,
    DateTimeOffset? ExpiresAt
);