using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;

/// <summary>
/// Represents the audit log repository in the CortiSense Platform.
/// </summary>
public interface IAuditLogRepository : IBaseRepository<AuditLog>
{
    Task<IEnumerable<AuditLog>> FindByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken);

    Task<IEnumerable<AuditLog>> FindByActorUserIdAsync(int actorUserId, CancellationToken cancellationToken);
}