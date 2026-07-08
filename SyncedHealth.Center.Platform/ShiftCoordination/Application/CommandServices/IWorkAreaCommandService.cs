using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

/// <summary>
/// Represents the work area command service in the CortiSense Platform.
/// </summary>
public interface IWorkAreaCommandService
{
    Task<Result<WorkArea>> Handle(
        CreateWorkAreaCommand command,
        CancellationToken cancellationToken = default
    );
}