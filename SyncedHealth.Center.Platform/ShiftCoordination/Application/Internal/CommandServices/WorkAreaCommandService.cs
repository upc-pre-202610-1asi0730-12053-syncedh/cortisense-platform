using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;

public class WorkAreaCommandService(
    IWorkAreaRepository workAreaRepository,
    IUnitOfWork unitOfWork)
    : IWorkAreaCommandService
{
    public async Task<Result<WorkArea>> Handle(
        CreateWorkAreaCommand command,
        CancellationToken cancellationToken = default)
    {
        var workArea = new WorkArea(command);

        try
        {
            await workAreaRepository.AddAsync(workArea, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<WorkArea>.Success(workArea);
        }
        catch (OperationCanceledException)
        {
            return Result<WorkArea>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<WorkArea>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while creating the work area."
            );
        }
        catch (Exception)
        {
            return Result<WorkArea>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while creating the work area."
            );
        }
    }
}