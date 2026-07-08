namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

/// <summary>
/// Command to create work area.
/// </summary>
public record CreateWorkAreaCommand(
    string Name
);