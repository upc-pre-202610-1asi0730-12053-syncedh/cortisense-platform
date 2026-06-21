namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

public record CreateInvitationCommand(
    int OrganizationId,
    string Email,
    string Role,
    string? Status,
    string? Token,
    DateTimeOffset? ExpiresAt
);