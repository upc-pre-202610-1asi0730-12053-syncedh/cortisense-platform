using SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;

namespace SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.QueryServices;

/// <summary>
/// Application service that handles audit log read operations.
/// </summary>
public class AuditLogQueryService(IAuditLogRepository auditLogRepository) : IAuditLogQueryService
{
    /// <inheritdoc />
    public async Task<AuditLog?> Handle(
        GetAuditLogByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByIdAsync(query.AuditLogId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLog>> Handle(
        GetAllAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLog>> Handle(
        GetAuditLogsByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLog>> Handle(
        GetAuditLogsByActorUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await auditLogRepository.FindByActorUserIdAsync(
            query.ActorUserId,
            cancellationToken);
    }
}