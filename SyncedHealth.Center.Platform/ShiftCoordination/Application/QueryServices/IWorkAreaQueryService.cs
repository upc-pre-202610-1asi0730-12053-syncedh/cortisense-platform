using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

public interface IWorkAreaQueryService
{
    Task<IEnumerable<WorkArea>> Handle(
        GetAllWorkAreasQuery query,
        CancellationToken cancellationToken = default
    );

    Task<WorkArea?> Handle(
        GetWorkAreaByIdQuery query,
        CancellationToken cancellationToken = default
    );

}