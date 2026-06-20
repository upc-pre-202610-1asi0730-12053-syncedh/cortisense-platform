using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;

public class ShiftRecordQueryService(IShiftRecordRepository shiftRecordRepository)
    : IShiftRecordQueryService
{
    public async Task<IEnumerable<ShiftRecord>> Handle(
        GetAllShiftRecordsQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.ListAsync(cancellationToken);
    }

    public async Task<ShiftRecord?> Handle(
        GetShiftRecordByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> Handle(
        GetShiftRecordsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.FindByOrganizationIdAsync(query.OrganizationId, cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> Handle(
        GetShiftRecordsByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> Handle(
        GetShiftRecordsByWorkAreaIdQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.FindByWorkAreaIdAsync(query.WorkAreaId, cancellationToken);
    }

    public async Task<IEnumerable<ShiftRecord>> Handle(
        GetShiftRecordsByStatusQuery query,
        CancellationToken cancellationToken)
    {
        return await shiftRecordRepository.FindByStatusAsync(query.Status, cancellationToken);
    }
}