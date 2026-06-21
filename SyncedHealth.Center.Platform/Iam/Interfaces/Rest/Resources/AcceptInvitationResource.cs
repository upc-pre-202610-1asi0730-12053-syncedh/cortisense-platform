namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record AcceptInvitationResource(
    string Token,
    string FirstName,
    string LastName,
    string Password,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId
);