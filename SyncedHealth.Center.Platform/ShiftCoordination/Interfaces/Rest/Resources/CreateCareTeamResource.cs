namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CreateCareTeamResource
{
    public int OrganizationId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int WorkAreaId { get; init; }
    public int? SupervisorId { get; init; }
    public string Status { get; init; } = "ACTIVE";
}