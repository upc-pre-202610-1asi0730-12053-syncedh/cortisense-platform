using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

public partial class TeamMember
{
    public TeamMember()
    {
    }

    public TeamMember(CreateTeamMemberCommand command)
    {
        TeamId = command.TeamId;
        UserId = command.UserId;
    }

    public int Id { get; set; }

    public int TeamId { get; set; }

    public int UserId { get; set; }
}