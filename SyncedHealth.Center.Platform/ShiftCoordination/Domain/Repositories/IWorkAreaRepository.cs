using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

public interface IWorkAreaRepository : IBaseRepository<WorkArea>
{
    Task<IEnumerable<WorkArea>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );
}