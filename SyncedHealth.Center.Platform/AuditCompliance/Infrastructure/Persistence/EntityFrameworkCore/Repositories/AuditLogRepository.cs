using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.AuditCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
/// Entity Framework Core repository implementation for audit logs.
/// </summary>
public class AuditLogRepository(AppDbContext context)
    : BaseRepository<AuditLog>(context), IAuditLogRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<AuditLog>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<AuditLog>()
            .Where(auditLog => auditLog.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLog>> FindByActorUserIdAsync(
        int actorUserId,
        CancellationToken cancellationToken)
    {
        return await Context.Set<AuditLog>()
            .Where(auditLog => auditLog.ActorUserId == actorUserId)
            .ToListAsync(cancellationToken);
    }
}