namespace SyncedHealth.Center.Platform.Iam.Application.OutboundServices;

/// <summary>
/// Represents the invitation email service in the CortiSense Platform.
/// </summary>
public interface IInvitationEmailService
{
    Task<InvitationEmailResult> SendInvitationAsync(
        string email,
        string token,
        string role,
        string organizationName,
        CancellationToken cancellationToken
    );
}