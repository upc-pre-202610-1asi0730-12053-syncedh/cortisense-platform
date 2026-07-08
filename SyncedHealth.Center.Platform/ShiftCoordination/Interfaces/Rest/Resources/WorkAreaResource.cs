namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the work area resource in the CortiSense Platform.
/// </summary>
public record WorkAreaResource
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}