using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;

namespace SyncedHealth.Center.Platform.Iam.Application.QueryServices;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken);

    Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<User>> Handle(GetUsersByOrganizationIdQuery query, CancellationToken cancellationToken);

    // Temporary compatibility with old query name.
    Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken);
}