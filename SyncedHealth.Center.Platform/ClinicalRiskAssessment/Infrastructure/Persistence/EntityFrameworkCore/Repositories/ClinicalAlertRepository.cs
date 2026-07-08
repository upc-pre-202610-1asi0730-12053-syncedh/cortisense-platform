using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Represents the clinical alert repository in the CortiSense Platform.
/// </summary>
public class ClinicalAlertRepository(AppDbContext context)
    : BaseRepository<ClinicalAlert>(context), IClinicalAlertRepository
{
    public async Task<IEnumerable<ClinicalAlert>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ClinicalAlert>()
            .Where(alert => alert.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> FindByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ClinicalAlert>()
            .Where(alert => alert.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> FindByStatusAsync(
        string status,
        CancellationToken cancellationToken = default)
    {
        var normalizedStatus = status.ToUpperInvariant();

        return await Context.Set<ClinicalAlert>()
            .Where(alert => alert.Status == normalizedStatus)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ClinicalAlert>> FindBySeverityAsync(
        string severity,
        CancellationToken cancellationToken = default)
    {
        var normalizedSeverity = severity.ToUpperInvariant();

        return await Context.Set<ClinicalAlert>()
            .Where(alert => alert.Severity == normalizedSeverity)
            .ToListAsync(cancellationToken);
    }
}