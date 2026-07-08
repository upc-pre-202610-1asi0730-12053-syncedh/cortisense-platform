using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices;

/// <summary>
/// Represents the audit log command service in the CortiSense Platform.
/// </summary>
public interface IAuditLogCommandService
{
    Task<Result<AuditLog>> Handle(CreateAuditLogCommand command, CancellationToken cancellationToken);
}