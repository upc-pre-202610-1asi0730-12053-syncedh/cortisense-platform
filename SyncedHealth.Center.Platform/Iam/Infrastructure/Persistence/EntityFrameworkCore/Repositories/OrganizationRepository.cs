using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class OrganizationRepository(AppDbContext context)
    : BaseRepository<Organization>(context), IOrganizationRepository
{
    public async Task<Organization?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(
                organization => organization.Email == normalizedEmail,
                cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<Organization>()
            .AnyAsync(
                organization => organization.Email == normalizedEmail,
                cancellationToken);
    }

    public async Task<bool> ExistsByRucAsync(
        string ruc,
        CancellationToken cancellationToken = default)
    {
        var normalizedRuc = ruc.Trim();

        return await Context.Set<Organization>()
            .AnyAsync(
                organization => organization.Ruc == normalizedRuc,
                cancellationToken);
    }
}