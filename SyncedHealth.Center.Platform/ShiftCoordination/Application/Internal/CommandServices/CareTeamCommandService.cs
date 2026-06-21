using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Commands;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Repositories;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Application.Internal.CommandServices;

public class CareTeamCommandService(
    ICareTeamRepository careTeamRepository,
    IUnitOfWork unitOfWork)
    : ICareTeamCommandService
{
    public async Task<Result<CareTeam>> Handle(
        CreateCareTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var isActive = command.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);

        if (isActive && await careTeamRepository.ExistsActiveBySupervisorIdAsync(
                command.SupervisorId,
                null,
                cancellationToken))
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.SupervisorAlreadyAssignedToActiveTeam,
                "The supervisor is already assigned to another active team."
            );
        }

        var careTeam = new CareTeam(command);

        try
        {
            await careTeamRepository.AddAsync(careTeam, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<CareTeam>.Success(careTeam);
        }
        catch (OperationCanceledException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while creating the care team."
            );
        }
        catch (Exception)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while creating the care team."
            );
        }
    }

    public async Task<Result<CareTeam>> Handle(
        UpdateCareTeamCommand command,
        CancellationToken cancellationToken = default)
    {
        var careTeam = await careTeamRepository.FindByIdAsync(command.Id, cancellationToken);

        if (careTeam is null)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.CareTeamNotFound,
                "The specified care team was not found."
            );
        }

        var isActive = command.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase);

        if (isActive && await careTeamRepository.ExistsActiveBySupervisorIdAsync(
                command.SupervisorId,
                command.Id,
                cancellationToken))
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.SupervisorAlreadyAssignedToActiveTeam,
                "The supervisor is already assigned to another active team."
            );
        }

        careTeam.Update(command);

        try
        {
            careTeamRepository.Update(careTeam);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<CareTeam>.Success(careTeam);
        }
        catch (OperationCanceledException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while updating the care team."
            );
        }
        catch (Exception)
        {
            return Result<CareTeam>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while updating the care team."
            );
        }
    }
}