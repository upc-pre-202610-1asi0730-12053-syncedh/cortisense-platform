using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Domain.Repositories;

namespace SyncedHealth.Center.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByEmailAsync(query.Email, cancellationToken);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.ListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> Handle(
        GetUsersByOrganizationIdQuery query,
        CancellationToken cancellationToken)
    {
        return await userRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }

    public async Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
    {
        return await userRepository.FindByEmailAsync(query.Username, cancellationToken);
    }
}