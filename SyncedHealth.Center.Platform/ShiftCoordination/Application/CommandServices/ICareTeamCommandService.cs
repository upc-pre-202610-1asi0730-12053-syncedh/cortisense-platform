using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

/// <summary>
/// Represents the care team command service in the CortiSense Platform.
/// </summary>
public interface ICareTeamCommandService
{
    Task<Result<CareTeam>> Handle(
        CreateCareTeamCommand command,
        CancellationToken cancellationToken = default
    );

    Task<Result<CareTeam>> Handle(
        UpdateCareTeamCommand command,
        CancellationToken cancellationToken = default
    );

    Task<Result<CareTeam>> Handle(
        DeleteCareTeamCommand command,
        CancellationToken cancellationToken = default
    );
}