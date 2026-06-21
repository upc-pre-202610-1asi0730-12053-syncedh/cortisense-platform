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

public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    IStringLocalizer<IamMessages> iamLocalizer)
    : IOrganizationCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly IStringLocalizer<IamMessages> _iamLocalizer = iamLocalizer;

    private static readonly string[] ValidStatuses =
    [
        "ACTIVE",
        "INACTIVE",
        "PENDING",
        "CANCELLED"
    ];

    private static readonly string[] ValidRegistrationStatuses =
    [
        "PENDING",
        "COMPLETED",
        "CANCELLED"
    ];

    public async Task<Result<Organization>> Handle(
        CreateOrganizationCommand command,
        CancellationToken cancellationToken)
    {
        var validation = ValidateCreateOrganization(command);
        if (validation is not null) return validation;

        var email = command.Email.Trim().ToLowerInvariant();
        var ruc = command.Ruc.Trim();

        if (await organizationRepository.ExistsByEmailAsync(email, cancellationToken))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationEmailAlreadyExists"].Value
            );

        if (await organizationRepository.ExistsByRucAsync(ruc, cancellationToken))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationRucAlreadyExists"].Value
            );

        var organization = new Organization(command);

        try
        {
            await organizationRepository.AddAsync(organization, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Organization>.Success(organization);
        }
        catch (OperationCanceledException)
        {
            return Result<Organization>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Organization>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    public async Task<Result<Organization>> Handle(
        UpdateOrganizationCommand command,
        CancellationToken cancellationToken)
    {
        var organization = await organizationRepository.FindByIdAsync(
            command.Id,
            cancellationToken);

        if (organization is null)
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationNotFound"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            var normalizedEmail = command.Email.Trim().ToLowerInvariant();
            var existing = await organizationRepository.FindByEmailAsync(
                normalizedEmail,
                cancellationToken);

            if (existing is not null && existing.Id != command.Id)
                return Result<Organization>.Failure(
                    IamError.InternalServerError,
                    _iamLocalizer["OrganizationEmailAlreadyExists"].Value
                );
        }

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidOrganizationStatus"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.RegistrationStatus) &&
            !ValidRegistrationStatuses.Contains(command.RegistrationStatus.ToUpperInvariant()))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidRegistrationStatus"].Value
            );

        organization.Update(command);

        try
        {
            organizationRepository.Update(organization);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Organization>.Success(organization);
        }
        catch (OperationCanceledException)
        {
            return Result<Organization>.Failure(
                IamError.OperationCancelled,
                GetErrorMessage(IamError.OperationCancelled)
            );
        }
        catch (DbUpdateException)
        {
            return Result<Organization>.Failure(
                IamError.DatabaseError,
                GetErrorMessage(IamError.DatabaseError)
            );
        }
        catch (Exception)
        {
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                GetErrorMessage(IamError.InternalServerError)
            );
        }
    }

    private Result<Organization>? ValidateCreateOrganization(CreateOrganizationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationNameRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Ruc))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationRucRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationEmailRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Phone))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationPhoneRequired"].Value
            );

        if (string.IsNullOrWhiteSpace(command.Address))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["OrganizationAddressRequired"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.Status) &&
            !ValidStatuses.Contains(command.Status.ToUpperInvariant()))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidOrganizationStatus"].Value
            );

        if (!string.IsNullOrWhiteSpace(command.RegistrationStatus) &&
            !ValidRegistrationStatuses.Contains(command.RegistrationStatus.ToUpperInvariant()))
            return Result<Organization>.Failure(
                IamError.InternalServerError,
                _iamLocalizer["InvalidRegistrationStatus"].Value
            );

        return null;
    }

    private string GetErrorMessage(IamError error)
    {
        return _errorLocalizer[$"{nameof(IamError)}.{error}"].Value;
    }
}