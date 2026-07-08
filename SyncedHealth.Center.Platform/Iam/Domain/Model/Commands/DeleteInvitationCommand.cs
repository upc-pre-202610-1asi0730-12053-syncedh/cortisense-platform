namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to delete invitation.
/// </summary>
public record DeleteInvitationCommand(
    int Id
);