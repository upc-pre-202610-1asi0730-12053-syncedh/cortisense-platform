using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

/// <summary>
/// Represents the care team query service in the CortiSense Platform.
/// </summary>
public interface ICareTeamQueryService
{
    Task<IEnumerable<CareTeam>> Handle(
        GetAllCareTeamsQuery query,
        CancellationToken cancellationToken = default
    );

    Task<CareTeam?> Handle(
        GetCareTeamByIdQuery query,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsByOrganizationIdQuery query,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsBySupervisorIdQuery query,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<CareTeam>> Handle(
        GetCareTeamsByWorkAreaIdQuery query,
        CancellationToken cancellationToken = default
    );
}