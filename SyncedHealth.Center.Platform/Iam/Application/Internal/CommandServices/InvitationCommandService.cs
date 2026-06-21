using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Iam.Resources;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;

namespace SyncedHealth.Center.Platform.Iam.Application.Internal.CommandServices;

public class InvitationCommandService(
    IInvitationRepository invitationRepository,
    IUserCommandService userCommandService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer)
    : IInvitationCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly IStringLocalizer<IamMessages> _iamLocalizer = iamLocalizer;

    private static readonly string[] ValidRoles =
    [
        "ADMIN",
        "HOSPITAL_ADMIN",
        "SUPERVISOR",
        "CLINICAL_SUPERVISOR",
        "DOCTOR",
        "MEDICAL_STAFF"
    ];

    private static readonly string[] ValidStatuses =
    [
        "PENDING",
        "SENT",
        "ACCEPTED",
        "CANCELLED"
    ];

    private static readonly string[] ValidEmailStatuses =
    [
        "PENDING",
        "SENT",
        "FAILED",
        "SKIPPED"
    ];

    public async Task<Result<Invitation>> Handle(
        CreateInvitationCommand command,
        CancellationToken cancellationToken)
    {
        var validation = ValidateCreateInvitation(command);
        if (validation is not null) return validation;

        var email = command.Email.Trim().ToLowerInvariant();

        if (await invitationRepository.ExistsPendingByEmailAsync(
                command.OrganizationId,
                email,
                cancellationToken))
        {
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationAlreadyExists"].Value
            );
        }

        var invitation = new Invitation(command);

        try
        {
            await invitationRepository.AddAsync(invitation, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Invitation>.Success(invitation);
        }
        catch (OperationCanceledException)
        {
            return Result<Invitation>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Invitation>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<Invitation>> Handle(
        UpdateInvitationCommand command,
        CancellationToken cancellationToken)
    {
        var invitation = await invitationRepository.FindByIdAsync(
            command.Id,
            cancellationToken);

        if (invitation is null)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationNotFound"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Role) &&
            !ValidRoles.Contains(command.Role.Trim().ToUpperInvariant()))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserRole"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ValidStatuses.Contains(command.Status.Trim().ToUpperInvariant()))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidInvitationStatus"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.EmailStatus) &&
            !ValidEmailStatuses.Contains(command.EmailStatus.Trim().ToUpperInvariant()))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidInvitationEmailStatus"].Value
            );

        invitation.Update(command);

        try
        {
            invitationRepository.Update(invitation);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Invitation>.Success(invitation);
        }
        catch (OperationCanceledException)
        {
            return Result<Invitation>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Invitation>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<Invitation>> Handle(
        DeleteInvitationCommand command,
        CancellationToken cancellationToken)
    {
        var invitation = await invitationRepository.FindByIdAsync(
            command.Id,
            cancellationToken);

        if (invitation is null)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationNotFound"].Value
            );

        invitation.Cancel();

        try
        {
            invitationRepository.Update(invitation);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Invitation>.Success(invitation);
        }
        catch (OperationCanceledException)
        {
            return Result<Invitation>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Invitation>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<Invitation>> Handle(
        AcceptInvitationCommand command,
        CancellationToken cancellationToken)
    {
        var validation = ValidateAcceptInvitation(command);
        if (validation is not null) return validation;

        var invitation = await invitationRepository.FindByTokenAsync(
            command.Token,
            cancellationToken);

        if (invitation is null)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationNotFound"].Value
            );

        if (invitation.Status == "ACCEPTED")
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationAlreadyAccepted"].Value
            );

        if (invitation.Status == "CANCELLED")
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationCancelled"].Value
            );

        if (invitation.ExpiresAt.HasValue && invitation.ExpiresAt.Value < DateTimeOffset.UtcNow)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationExpired"].Value
            );

        var signUpCommand = new SignUpCommand(
            invitation.OrganizationId,
            command.FirstName,
            command.LastName,
            invitation.Email,
            command.Password,
            invitation.Role,
            "ACTIVE",
            command.Phone,
            command.WorkAreaId,
            command.SpecialtyId,
            "COMPLETED"
        );

        var userResult = await userCommandService.Handle(signUpCommand, cancellationToken);

        if (userResult.IsFailure)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                userResult.Message
            );

        invitation.MarkAsAccepted();

        try
        {
            invitationRepository.Update(invitation);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Invitation>.Success(invitation);
        }
        catch (OperationCanceledException)
        {
            return Result<Invitation>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Invitation>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    private Result<Invitation>? ValidateCreateInvitation(CreateInvitationCommand command)
    {
        if (command.OrganizationId <= 0)
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationIdMustBeValid"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationEmailRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Role))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationRoleRequired"].Value
            );

        if (!ValidRoles.Contains(command.Role.Trim().ToUpperInvariant()))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserRole"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ValidStatuses.Contains(command.Status.Trim().ToUpperInvariant()))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidInvitationStatus"].Value
            );

        return null;
    }

    private Result<Invitation>? ValidateAcceptInvitation(AcceptInvitationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Token))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationTokenRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.FirstName))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationFirstNameRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.LastName))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationLastNameRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Password))
            return Result<Invitation>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvitationPasswordRequired"].Value
            );

        return null;
    }

    private string GetErrorMessage(IamError error)
    {
        return _errorLocalizer[$"{nameof(IamError)}.{error}"].Value;
    }
}