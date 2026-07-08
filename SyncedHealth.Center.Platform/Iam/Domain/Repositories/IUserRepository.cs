using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Domain.Repositories;

/// <summary>
/// Represents the user repository in the CortiSense Platform.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<User>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    // Temporary compatibility with old IAM names.
    Task<User?> FindByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default
    );
}