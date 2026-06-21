namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record CreateInvitationResource(
    int OrganizationId,
    string Email,
    string Role,
    string? Status,
    string? Token,
    DateTimeOffset? ExpiresAt
);