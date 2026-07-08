using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;

/// <summary>
/// Represents the shift record command service in the CortiSense Platform.
/// </summary>
public interface IShiftRecordCommandService
{
    Task<Result<ShiftRecord>> Handle(CreateShiftRecordCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(UpdateShiftRecordStatusCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(CheckInShiftRecordCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(CheckOutShiftRecordCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(EvaluateCriticalShiftCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(BlockShiftCommand command, CancellationToken cancellationToken);

    Task<Result<ShiftRecord>> Handle(ReassignShiftCommand command, CancellationToken cancellationToken);
}