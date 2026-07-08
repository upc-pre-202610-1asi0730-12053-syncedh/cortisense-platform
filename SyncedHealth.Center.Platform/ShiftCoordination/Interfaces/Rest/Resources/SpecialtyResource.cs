namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the specialty resource in the CortiSense Platform.
/// </summary>
public record SpecialtyResource
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}