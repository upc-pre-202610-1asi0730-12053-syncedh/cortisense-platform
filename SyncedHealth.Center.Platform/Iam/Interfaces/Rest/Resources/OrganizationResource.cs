namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

public record OrganizationResource(
    int Id,
    string Name,
    string Ruc,
    string Email,
    string Phone,
    string Address,
    string Status,
    string RegistrationStatus,
    DateTimeOffset? ActivatedAt,
    DateTimeOffset? CancelledAt,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt
);