namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

public record AcceptInvitationCommand(
    string Token,
    string FirstName,
    string LastName,
    string Password,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId
);