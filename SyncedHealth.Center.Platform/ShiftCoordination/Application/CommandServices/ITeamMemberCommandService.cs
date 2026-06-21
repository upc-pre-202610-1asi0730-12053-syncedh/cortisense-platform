using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

public interface ITeamMemberCommandService
{
    Task<Result<TeamMember>> Handle(
        CreateTeamMemberCommand command,
        CancellationToken cancellationToken = default
    );

    Task<Result<TeamMember>> Handle(
        DeleteTeamMemberCommand command,
        CancellationToken cancellationToken = default
    );
}