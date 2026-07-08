using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Represents the vital sign reading repository in the CortiSense Platform.
/// </summary>
public class VitalSignReadingRepository(AppDbContext context)
    : BaseRepository<VitalSignReading>(context), IVitalSignReadingRepository
{
    public async Task<IEnumerable<VitalSignReading>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<VitalSignReading>()
            .Where(reading => reading.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<VitalSignReading>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<VitalSignReading>()
            .Where(reading => reading.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}