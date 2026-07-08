using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.QueryServices;

/// <summary>
/// Represents the specialty query service in the CortiSense Platform.
/// </summary>
public class SpecialtyQueryService(ISpecialtyRepository specialtyRepository)
    : ISpecialtyQueryService
{
    public async Task<IEnumerable<Specialty>> Handle(
        GetAllSpecialtiesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await specialtyRepository.ListAsync(cancellationToken);
    }

    public async Task<Specialty?> Handle(
        GetSpecialtyByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await specialtyRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}