using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Iam.Application.QueryServices;

/// <summary>
/// Represents the organization query service in the CortiSense Platform.
/// </summary>
public interface IOrganizationQueryService
{
    Task<IEnumerable<Organization>> Handle(
        GetAllOrganizationsQuery query,
        CancellationToken cancellationToken
    );

    Task<Organization?> Handle(
        GetOrganizationByIdQuery query,
        CancellationToken cancellationToken
    );

    Task<Organization?> Handle(
        GetOrganizationByEmailQuery query,
        CancellationToken cancellationToken
    );
}