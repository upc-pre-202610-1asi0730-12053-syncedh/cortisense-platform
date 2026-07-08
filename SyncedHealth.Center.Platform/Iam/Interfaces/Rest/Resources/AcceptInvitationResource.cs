namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the accept invitation resource in the CortiSense Platform.
/// </summary>
public record AcceptInvitationResource(
    string Token,
    string FirstName,
    string LastName,
    string Password,
    string? Phone,
    int? WorkAreaId,
    int? SpecialtyId
);