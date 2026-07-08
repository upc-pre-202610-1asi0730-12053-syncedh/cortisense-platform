using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;

/// <summary>
/// Represents the specialty command service in the CortiSense Platform.
/// </summary>
public class SpecialtyCommandService(
    ISpecialtyRepository specialtyRepository,
    IUnitOfWork unitOfWork)
    : ISpecialtyCommandService
{
    public async Task<Result<Specialty>> Handle(
        CreateSpecialtyCommand command,
        CancellationToken cancellationToken = default)
    {
        var specialty = new Specialty(command);

        try
        {
            await specialtyRepository.AddAsync(specialty, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Specialty>.Success(specialty);
        }
        catch (OperationCanceledException)
        {
            return Result<Specialty>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<Specialty>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while creating the specialty."
            );
        }
        catch (Exception)
        {
            return Result<Specialty>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while creating the specialty."
            );
        }
    }
}