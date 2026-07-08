using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

/// <summary>
/// Represents the care team in the CortiSense Platform.
/// </summary>
public partial class CareTeam
{
    public CareTeam()
    {
        Name = string.Empty;
        Status = string.Empty;
    }

    public CareTeam(CreateCareTeamCommand command)
    {
        OrganizationId = command.OrganizationId;
        Name = command.Name.Trim();
        WorkAreaId = command.WorkAreaId;
        SupervisorId = command.SupervisorId;
        Status = command.Status.ToUpperInvariant();
    }

    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public string Name { get; set; }

    public int WorkAreaId { get; set; }

    public int SupervisorId { get; set; }

    public string Status { get; set; }

    public void Update(UpdateCareTeamCommand command)
    {
        Name = command.Name.Trim();
        WorkAreaId = command.WorkAreaId;
        SupervisorId = command.SupervisorId;
        Status = command.Status.ToUpperInvariant();
    }
}