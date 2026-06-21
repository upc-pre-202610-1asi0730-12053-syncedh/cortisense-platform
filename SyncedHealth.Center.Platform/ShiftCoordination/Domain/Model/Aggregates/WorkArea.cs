using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

public partial class WorkArea
{
    public WorkArea()
    {
        Name = string.Empty;
    }

    public WorkArea(CreateWorkAreaCommand command)
    {
        OrganizationId = command.OrganizationId;
        Name = command.Name.Trim();
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public string Name { get; set; }
}