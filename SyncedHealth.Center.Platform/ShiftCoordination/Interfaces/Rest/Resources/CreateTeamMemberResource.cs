namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create team member resource in the CortiSense Platform.
/// </summary>
public record CreateTeamMemberResource
{
    public int TeamId { get; init; }
    public int UserId { get; init; }
}