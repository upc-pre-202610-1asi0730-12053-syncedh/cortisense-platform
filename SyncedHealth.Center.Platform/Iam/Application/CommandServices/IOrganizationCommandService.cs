using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.Iam.Application.CommandServices;

/// <summary>
/// Represents the organization command service in the CortiSense Platform.
/// </summary>
public interface IOrganizationCommandService
{
    Task<Result<Organization>> Handle(
        CreateOrganizationCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<Organization>> Handle(
        UpdateOrganizationCommand command,
        CancellationToken cancellationToken
    );
}