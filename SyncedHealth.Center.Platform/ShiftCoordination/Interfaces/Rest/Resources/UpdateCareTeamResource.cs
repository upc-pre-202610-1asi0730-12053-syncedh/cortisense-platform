namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the update care team resource in the CortiSense Platform.
/// </summary>
public record UpdateCareTeamResource
{
    public string? Name { get; init; }
    public int? WorkAreaId { get; init; }
    public int? SupervisorId { get; init; }
    public string? Status { get; init; }
}