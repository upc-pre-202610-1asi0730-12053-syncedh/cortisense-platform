namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record UpdateCareTeamResource
{
    public string? Name { get; init; }
    public int? WorkAreaId { get; init; }
    public int? SupervisorId { get; init; }
    public string? Status { get; init; }
}