using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Domain.Repositories;

public interface IOrganizationRepository : IBaseRepository<Organization>
{
    Task<Organization?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByRucAsync(
        string ruc,
        CancellationToken cancellationToken = default
    );
}