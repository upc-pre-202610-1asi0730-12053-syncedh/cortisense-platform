using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

/// <summary>
/// Represents the specialty repository in the CortiSense Platform.
/// </summary>
public interface ISpecialtyRepository : IBaseRepository<Specialty>
{
}