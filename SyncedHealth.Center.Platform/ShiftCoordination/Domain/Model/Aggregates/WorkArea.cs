using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

/// <summary>
/// Represents the work area in the CortiSense Platform.
/// </summary>
public partial class WorkArea
{
    public WorkArea()
    {
        Name = string.Empty;
    }

    public WorkArea(CreateWorkAreaCommand command)
    {
        Name = command.Name.Trim();
    }

    public int Id { get; set; }

    public string Name { get; set; }
}