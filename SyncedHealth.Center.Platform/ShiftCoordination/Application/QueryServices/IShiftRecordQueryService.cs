using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

public interface IShiftRecordQueryService
{
    Task<IEnumerable<ShiftRecord>> Handle(GetAllShiftRecordsQuery query, CancellationToken cancellationToken);

    Task<ShiftRecord?> Handle(GetShiftRecordByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<ShiftRecord>> Handle(GetShiftRecordsByOrganizationIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<ShiftRecord>> Handle(GetShiftRecordsByUserIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<ShiftRecord>> Handle(GetShiftRecordsByWorkAreaIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<ShiftRecord>> Handle(GetShiftRecordsByStatusQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<ShiftRecord>> Handle(GetReplacementSuggestionsQuery query, CancellationToken cancellationToken);
}