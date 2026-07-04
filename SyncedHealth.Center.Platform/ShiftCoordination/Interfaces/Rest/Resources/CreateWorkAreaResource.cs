namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CreateWorkAreaResource
{
    public string Name { get; init; } = string.Empty;
}