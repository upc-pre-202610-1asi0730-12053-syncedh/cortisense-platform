namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CreateSpecialtyResource
{
    public string Name { get; init; } = string.Empty;
}