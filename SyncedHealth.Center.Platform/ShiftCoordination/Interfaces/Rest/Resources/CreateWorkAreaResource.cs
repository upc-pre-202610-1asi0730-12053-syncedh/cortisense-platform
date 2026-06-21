namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;

public record CreateWorkAreaResource
{
    public int OrganizationId { get; init; }
    public string Name { get; init; } = string.Empty;
}