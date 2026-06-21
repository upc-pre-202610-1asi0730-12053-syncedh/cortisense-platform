namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

public record UpdateInvitationCommand(
    int Id,
    string? Email,
    string? Role,
    string? Status,
    string? EmailStatus,
    string? ResendEmailId,
    string? EmailError,
    DateTimeOffset? ExpiresAt
);