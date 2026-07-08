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
/// Represents the team member command service in the CortiSense Platform.
/// </summary>
public class TeamMemberCommandService(
    ITeamMemberRepository teamMemberRepository,
    ICareTeamRepository careTeamRepository,
    IUnitOfWork unitOfWork)
    : ITeamMemberCommandService
{
    public async Task<Result<TeamMember>> Handle(
        CreateTeamMemberCommand command,
        CancellationToken cancellationToken = default)
    {
        var careTeam = await careTeamRepository.FindByIdAsync(command.TeamId, cancellationToken);

        if (careTeam is null)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.CareTeamNotFound,
                "The specified care team was not found."
            );
        }

        if (!careTeam.Status.Equals("ACTIVE", StringComparison.OrdinalIgnoreCase))
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.InactiveCareTeam,
                "Members cannot be added to an inactive care team."
            );
        }

        if (await teamMemberRepository.ExistsByUserIdAsync(command.UserId, cancellationToken))
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.UserAlreadyAssignedToTeam,
                "The user is already assigned to another team."
            );
        }

        var teamMember = new TeamMember(command);

        try
        {
            await teamMemberRepository.AddAsync(teamMember, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<TeamMember>.Success(teamMember);
        }
        catch (OperationCanceledException)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while creating the team member."
            );
        }
        catch (Exception)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while creating the team member."
            );
        }
    }

    public async Task<Result<TeamMember>> Handle(
        DeleteTeamMemberCommand command,
        CancellationToken cancellationToken = default)
    {
        var teamMember = await teamMemberRepository.FindByIdAsync(command.Id, cancellationToken);

        if (teamMember is null)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.TeamMemberNotFound,
                "The specified team member was not found."
            );
        }

        try
        {
            teamMemberRepository.Remove(teamMember);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<TeamMember>.Success(teamMember);
        }
        catch (OperationCanceledException)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.OperationCancelled,
                "The operation was cancelled."
            );
        }
        catch (DbUpdateException)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.DatabaseError,
                "A database error occurred while deleting the team member."
            );
        }
        catch (Exception)
        {
            return Result<TeamMember>.Failure(
                ShiftCoordinationError.InternalServerError,
                "An internal server error occurred while deleting the team member."
            );
        }
    }
}