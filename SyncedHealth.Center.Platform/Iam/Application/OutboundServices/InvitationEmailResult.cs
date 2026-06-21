namespace SyncedHealth.Center.Platform.Iam.Application.OutboundServices;

public record InvitationEmailResult(
    bool Sent,
    string EmailStatus,
    string? ResendEmailId,
    string? ErrorMessage
);