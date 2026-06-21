using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InvitationRepository(AppDbContext context)
    : BaseRepository<Invitation>(context), IInvitationRepository
{
    public async Task<Invitation?> FindByTokenAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var normalizedToken = token.Trim();

        return await Context.Set<Invitation>()
            .FirstOrDefaultAsync(
                invitation => invitation.Token == normalizedToken,
                cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Invitation>()
            .Where(invitation => invitation.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<Invitation>()
            .Where(invitation => invitation.Email == normalizedEmail)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsPendingByEmailAsync(
        int organizationId,
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<Invitation>()
            .AnyAsync(
                invitation =>
                    invitation.OrganizationId == organizationId &&
                    invitation.Email == normalizedEmail &&
                    (invitation.Status == "PENDING" || invitation.Status == "SENT"),
                cancellationToken);
    }
}