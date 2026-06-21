namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record UpdateInvitationResource(
    string? Email,
    string? Role,
    string? Status,
    string? EmailStatus,
    string? ResendEmailId,
    string? EmailError,
    DateTimeOffset? ExpiresAt
);