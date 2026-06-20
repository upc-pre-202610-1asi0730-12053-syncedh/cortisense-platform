using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Repositories;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Services;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.AuditCompliance.Application.Internal.CommandServices;

/// <summary>
/// Application service that handles audit log write operations.
/// </summary>
public class AuditLogCommandService(
    IAuditLogRepository auditLogRepository,
    IUnitOfWork unitOfWork) : IAuditLogCommandService
{
    /// <inheritdoc />
    public async Task<Result<AuditLog>> Handle(
        CreateAuditLogCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var auditLog = new AuditLog(command);

            await auditLogRepository.AddAsync(auditLog, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<AuditLog>.Success(auditLog);
        }
        catch (ArgumentException exception)
        {
            return Result<AuditLog>.Failure(
                AuditComplianceError.InvalidAuditLogData,
                exception.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<AuditLog>.Failure(
                AuditComplianceError.OperationCancelled,
                "The audit log creation operation was cancelled.");
        }
        catch (Exception)
        {
            return Result<AuditLog>.Failure(
                AuditComplianceError.AuditLogCreationFailed,
                "An error occurred while creating the audit log.");
        }
    }
}