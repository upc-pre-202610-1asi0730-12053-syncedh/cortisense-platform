namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record InvitationResource(
    int Id,
    int OrganizationId,
    string Email,
    string Role,
    string Status,
    string Token,
    string EmailStatus,
    string? ResendEmailId,
    string? EmailError,
    DateTimeOffset? ExpiresAt,
    DateTimeOffset? SentAt,
    DateTimeOffset? AcceptedAt,
    DateTimeOffset? CancelledAt,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);