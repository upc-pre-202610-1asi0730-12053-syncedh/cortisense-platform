namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the team member resource in the CortiSense Platform.
/// </summary>
public record TeamMemberResource
{
    public int Id { get; init; }
    public int TeamId { get; init; }
    public int UserId { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}