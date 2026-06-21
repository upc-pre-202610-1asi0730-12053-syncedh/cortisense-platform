using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;

public interface ISpecialtyQueryService
{
    Task<IEnumerable<Specialty>> Handle(
        GetAllSpecialtiesQuery query,
        CancellationToken cancellationToken = default
    );

    Task<Specialty?> Handle(
        GetSpecialtyByIdQuery query,
        CancellationToken cancellationToken = default
    );
}