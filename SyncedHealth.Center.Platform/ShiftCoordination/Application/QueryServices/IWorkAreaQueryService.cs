using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

/// <summary>
/// Represents the work area query service in the CortiSense Platform.
/// </summary>
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