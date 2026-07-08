using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;

/// <summary>
/// Represents the audit log query service in the CortiSense Platform.
/// </summary>
public interface IAuditLogQueryService
{
    Task<AuditLog?> Handle(GetAuditLogByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<AuditLog>> Handle(GetAllAuditLogsQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<AuditLog>> Handle(GetAuditLogsByOrganizationIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<AuditLog>> Handle(GetAuditLogsByActorUserIdQuery query, CancellationToken cancellationToken);
}