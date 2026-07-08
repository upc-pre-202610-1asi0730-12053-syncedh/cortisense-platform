using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

/// <summary>
/// Represents the team member query service in the CortiSense Platform.
/// </summary>
public interface ITeamMemberQueryService
{
    Task<IEnumerable<TeamMember>> Handle(
        GetAllTeamMembersQuery query,
        CancellationToken cancellationToken = default
    );

    Task<TeamMember?> Handle(
        GetTeamMemberByIdQuery query,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TeamMember>> Handle(
        GetTeamMembersByTeamIdQuery query,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TeamMember>> Handle(
        GetTeamMembersByUserIdQuery query,
        CancellationToken cancellationToken = default
    );
}