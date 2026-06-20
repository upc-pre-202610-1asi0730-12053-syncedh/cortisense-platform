using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;


namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Services;

/// <summary>
/// Command service contract for audit log write operations.
/// </summary>
public interface IAuditLogCommandService
{
    /// <summary>
    /// Handles the audit log creation command.
    /// </summary>
    /// <param name="command">The command containing the audit log creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created audit log wrapped in a result object.</returns>
    Task<Result<AuditLog>> Handle(CreateAuditLogCommand command, CancellationToken cancellationToken);
}