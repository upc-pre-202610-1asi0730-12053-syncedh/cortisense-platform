using Microsoft.EntityFrameworkCore;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail, cancellationToken);
    }

    public async Task<IEnumerable<User>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>()
            .Where(user => user.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        return await Context.Set<User>()
            .AnyAsync(user => user.Email == normalizedEmail, cancellationToken);
    }

    public async Task<User?> FindByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await FindByEmailAsync(username, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await ExistsByEmailAsync(username, cancellationToken);
    }
}