using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

/// <summary>
/// Represents the work area repository in the CortiSense Platform.
/// </summary>
public interface IWorkAreaRepository : IBaseRepository<WorkArea>
{
}