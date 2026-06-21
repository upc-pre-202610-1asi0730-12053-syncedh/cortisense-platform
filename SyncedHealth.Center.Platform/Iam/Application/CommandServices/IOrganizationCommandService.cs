using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.Iam.Application.CommandServices;

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