using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

public interface IShiftRecordCommandService
{
    Task<Result<ShiftRecord>> Handle(CreateShiftRecordCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(UpdateShiftRecordStatusCommand command, CancellationToken cancellationToken);
}