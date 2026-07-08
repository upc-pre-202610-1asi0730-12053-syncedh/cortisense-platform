using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Represents the vital sign anomaly repository in the CortiSense Platform.
/// </summary>
public class VitalSignAnomalyRepository(AppDbContext context)
    : BaseRepository<VitalSignAnomaly>(context), IVitalSignAnomalyRepository
{
    public async Task<IEnumerable<VitalSignAnomaly>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<VitalSignAnomaly>()
            .Where(anomaly => anomaly.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<VitalSignAnomaly>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<VitalSignAnomaly>()
            .Where(anomaly => anomaly.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<VitalSignAnomaly>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status.ToUpperInvariant();

        return await Context.Set<VitalSignAnomaly>()
            .Where(anomaly => anomaly.Status == normalizedStatus)
            .ToListAsync(cancellationToken);
    }
}