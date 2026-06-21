namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CareTeamResource
{
    public int Id { get; init; }
    public int OrganizationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int WorkAreaId { get; init; }
    public int SupervisorId { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}