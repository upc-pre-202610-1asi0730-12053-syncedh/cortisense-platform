namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create work area resource in the CortiSense Platform.
/// </summary>
public record CreateWorkAreaResource
{
    public string Name { get; init; } = string.Empty;
}