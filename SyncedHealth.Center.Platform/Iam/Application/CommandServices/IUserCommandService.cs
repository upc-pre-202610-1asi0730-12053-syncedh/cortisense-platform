using SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Shared.Application.Model;

namespace SyncedHealth.Center.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result<(User user, string token)>> Handle(
        SignInCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<User>> Handle(
        SignUpCommand command,
        CancellationToken cancellationToken
    );

    Task<Result<User>> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken
    );
}