using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

/// <summary>
/// Represents the shift record repository in the CortiSense Platform.
/// </summary>
public interface IShiftRecordRepository : IBaseRepository<ShiftRecord>
{
    Task<IEnumerable<ShiftRecord>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ShiftRecord>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ShiftRecord>> FindByWorkAreaIdAsync(
        int workAreaId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ShiftRecord>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ShiftRecord>> FindAvailableByWorkAreaAsync(
        int workAreaId,
        int organizationId,
        CancellationToken cancellationToken = default
    );
}