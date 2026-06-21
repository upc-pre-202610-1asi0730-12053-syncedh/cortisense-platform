using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;

public class CareTeamQueryService(ICareTeamRepository careTeamRepository)
    : ICareTeamQueryService
{
    public async Task<IEnumerable<CareTeam>> Handle(
        GetAllCareTeamsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await careTeamRepository.ListAsync(cancellationToken);
    }

    public async Task<CareTeam?> Handle(
        GetCareTeamByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await careTeamRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsByOrganizationIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await careTeamRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }

    public async Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsBySupervisorIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await careTeamRepository.FindBySupervisorIdAsync(
            query.SupervisorId,
            cancellationToken);
    }

    public async Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsByWorkAreaIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await careTeamRepository.FindByWorkAreaIdAsync(
            query.WorkAreaId,
            cancellationToken);
    }
}