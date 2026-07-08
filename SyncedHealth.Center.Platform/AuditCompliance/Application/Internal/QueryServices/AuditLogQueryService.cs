using SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;

namespace SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.QueryServices;

/// <summary>
/// Represents the audit log query service in the CortiSense Platform.
/// </summary>
public class AuditLogQueryService(IAuditLogRepository auditLogRepository) : IAuditLogQueryService
{
    public async Task<AuditLog?> Handle(
        GetAuditLogByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByIdAsync(query.AuditLogId);
    }

    public async Task<IEnumerable<AuditLog>> Handle(
        GetAllAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.ListAsync();
    }

    public async Task<IEnumerable<AuditLog>> Handle(
        GetAuditLogsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }

    public async Task<IEnumerable<AuditLog>> Handle(
        GetAuditLogsByActorUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByActorUserIdAsync(
            query.ActorUserId,
            cancellationToken);
    }
}