namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create specialty resource in the CortiSense Platform.
/// </summary>
public record CreateSpecialtyResource
{
    public string Name { get; init; } = string.Empty;
}