using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Domain.Repositories;

/// <summary>
/// Represents the invitation repository in the CortiSense Platform.
/// </summary>
public interface IInvitationRepository : IBaseRepository<Invitation>
{
    Task<Invitation?> FindByTokenAsync(
        string token,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Invitation>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Invitation>> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsPendingByEmailAsync(
        int organizationId,
        string email,
        CancellationToken cancellationToken = default
    );
}