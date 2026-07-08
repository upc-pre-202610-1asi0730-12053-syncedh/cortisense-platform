using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

/// <summary>
/// Represents the specialty command service in the CortiSense Platform.
/// </summary>
public interface ISpecialtyCommandService
{
    Task<Result<Specialty>> Handle(
        CreateSpecialtyCommand command,
        CancellationToken cancellationToken = default
    );
}