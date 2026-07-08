namespace SyncedHealth.Center.Platform.Iam.Application.OutboundServices;

/// <summary>
/// Represents the invitation email result in the CortiSense Platform.
/// </summary>
public record InvitationEmailResult(
    bool Sent,
    string EmailStatus,
    string? ResendEmailId,
    string? ErrorMessage
);