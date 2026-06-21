namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CreateTeamMemberResource
{
    public int TeamId { get; init; }
    public int UserId { get; init; }
}