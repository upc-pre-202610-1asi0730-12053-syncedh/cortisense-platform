using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;

public class TeamMemberQueryService(ITeamMemberRepository teamMemberRepository)
    : ITeamMemberQueryService
{
    public async Task<IEnumerable<TeamMember>> Handle(
        GetAllTeamMembersQuery query,
        CancellationToken cancellationToken = default)
    {
        return await teamMemberRepository.ListAsync(cancellationToken);
    }

    public async Task<TeamMember?> Handle(
        GetTeamMemberByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await teamMemberRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<TeamMember>> Handle(
        GetTeamMembersByTeamIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await teamMemberRepository.FindByTeamIdAsync(
            query.TeamId,
            cancellationToken);
    }

    public async Task<IEnumerable<TeamMember>> Handle(
        GetTeamMembersByUserIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await teamMemberRepository.FindByUserIdAsync(
            query.UserId,
            cancellationToken);
    }
}