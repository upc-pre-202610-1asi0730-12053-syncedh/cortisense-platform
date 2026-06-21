using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;

public class WorkAreaQueryService(IWorkAreaRepository workAreaRepository)
    : IWorkAreaQueryService
{
    public async Task<IEnumerable<WorkArea>> Handle(
        GetAllWorkAreasQuery query,
        CancellationToken cancellationToken = default)
    {
        return await workAreaRepository.ListAsync(cancellationToken);
    }

    public async Task<WorkArea?> Handle(
        GetWorkAreaByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await workAreaRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<WorkArea>> Handle(
        GetWorkAreasByOrganizationIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await workAreaRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }
}