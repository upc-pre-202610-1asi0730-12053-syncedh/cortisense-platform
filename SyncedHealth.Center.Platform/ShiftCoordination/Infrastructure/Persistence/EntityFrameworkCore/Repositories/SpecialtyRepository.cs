using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SpecialtyRepository(AppDbContext context)
    : BaseRepository<Specialty>(context), ISpecialtyRepository
{
}