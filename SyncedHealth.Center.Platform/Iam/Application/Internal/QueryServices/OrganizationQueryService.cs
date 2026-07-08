using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Application.Internal.QueryServices;

/// <summary>
/// Represents the organization query service in the CortiSense Platform.
/// </summary>
public class OrganizationQueryService(IOrganizationRepository organizationRepository)
    : IOrganizationQueryService
{
    public async Task<IEnumerable<Organization>> Handle(
        GetAllOrganizationsQuery query,
        CancellationToken cancellationToken)
    {
        return await organizationRepository.ListAsync(cancellationToken);
    }

    public async Task<Organization?> Handle(
        GetOrganizationByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await organizationRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<Organization?> Handle(
        GetOrganizationByEmailQuery query,
        CancellationToken cancellationToken)
    {
        return await organizationRepository.FindByEmailAsync(query.Email, cancellationToken);
    }
}