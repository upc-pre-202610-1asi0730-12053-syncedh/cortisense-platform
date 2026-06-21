namespace SyncedHealth.Center.Platform.Iam.Application.OutboundServices;

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