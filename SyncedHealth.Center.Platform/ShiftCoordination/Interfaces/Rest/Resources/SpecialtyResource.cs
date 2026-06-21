namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record SpecialtyResource
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}