using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Iam.Resources;
using SyncedHealth.Center.Platform.Shared.Application.Model;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;

namespace SyncedHealth.Center.Platform.Iam.Application.Internal.CommandServices;

/// <summary>
/// Represents the user command service in the CortiSense Platform.
/// </summary>
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer)
    : IUserCommandService
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
        "ACTIVE",
        "INACTIVE",
        "PENDING",
        "CANCELLED"
    ];

    public async Task<Result<(User user, string token)>> Handle(
        SignInCommand command,
        CancellationToken cancellationToken)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var user = await userRepository.FindByEmailAsync(email, cancellationToken);

        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(
                IamError.InvalidCredentials,
                GetErrorMessage(IamError.InvalidCredentials)
            );

        if (user.Status != "ACTIVE")
            return Result<(User user, string token)>.Failure(
                IamError.InvalidCredentials,
                GetErrorMessage(IamError.InvalidCredentials)
            );

        var token = tokenService.GenerateToken(user);

        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result<User>> Handle(
        SignUpCommand command,
        CancellationToken cancellationToken)
    {
        var validation = ValidateSignUp(command);
        if (validation is not null) return validation;

        var email = command.Email.Trim().ToLowerInvariant();

        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
            return Result<User>.Failure(
                IamError.UsernameAlreadyTaken,
                GetErrorMessage(IamError.UsernameAlreadyTaken, email)
            );

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command, hashedPassword);

        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<User>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<User>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.Id, cancellationToken);

        if (user is null)
            return Result<User>.Failure(
                IamError.UserNotFound,
                GetErrorMessage(IamError.UserNotFound)
            );

        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            var normalizedEmail = command.Email.Trim().ToLowerInvariant();
            var existing = await userRepository.FindByEmailAsync(normalizedEmail, cancellationToken);

            if (existing is not null && existing.Id != command.Id)
                return Result<User>.Failure(
                    IamError.UsernameAlreadyTaken,
                    GetErrorMessage(IamError.UsernameAlreadyTaken, normalizedEmail)
                );
        }

        if (!string.IsNullOrWhiteSpace(command.Role) &&
            !ValidRoles.Contains(command.Role.ToUpperInvariant()))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserRole"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserStatus"].Value
            );

        var passwordHash = string.IsNullOrWhiteSpace(command.Password)
            ? null
            : hashingService.HashPassword(command.Password);

        user.Update(command, passwordHash);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<User>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<User>> Handle(
        AssignRoleCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            return Result<User>.Failure(
                IamError.UserNotFound,
                GetErrorMessage(IamError.UserNotFound)
            );

        if (string.IsNullOrWhiteSpace(command.Role) ||
            !ValidRoles.Contains(command.Role.ToUpperInvariant()))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserRole"].Value
            );

        // Update the role
        user.Update(new UpdateUserCommand(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            null,
            user.Phone,
            user.WorkAreaId,
            user.SpecialtyId,
            command.Role.ToUpperInvariant(),
            user.Status,
            user.RegistrationStatus
        ), null);

        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<User>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    private Result<User>? ValidateSignUp(SignUpCommand command)
    {
        if (command.OrganizationId <= 0)
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationIdMustBeValid"].Value
            );

        if (string.IsNullOrWhiteSpace(command.FirstName))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["FirstNameRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.LastName))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["LastNameRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["EmailRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Password))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["PasswordRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Role) ||
            !ValidRoles.Contains(command.Role.ToUpperInvariant()))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserRole"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Status) ||
            !ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<User>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidUserStatus"].Value
            );

        return null;
    }

    private string GetErrorMessage(IamError error)
    {
        return _errorLocalizer[$"{nameof(IamError)}.{error}"].Value;
    }

    private string GetErrorMessage(IamError error, params object[] arguments)
    {
        return _errorLocalizer[$"{nameof(IamError)}.{error}", arguments].Value;
    }
}