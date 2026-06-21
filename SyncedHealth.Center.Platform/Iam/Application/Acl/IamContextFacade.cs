using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Interfaces.Acl;

namespace SyncedHealth.Center.Platform.Iam.Application.Acl;

public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService)
    : IIamContextFacade
{
    public async Task<int> CreateUser(
        int organizationId,
        string firstName,
        string lastName,
        string email,
        string password,
        string role,
        CancellationToken cancellationToken)
    {
        var signUpCommand = new SignUpCommand(
            organizationId,
            firstName,
            lastName,
            email,
            password,
            role,
            "ACTIVE",
            null,
            null,
            null,
            "COMPLETED"
        );

        var result = await userCommandService.Handle(signUpCommand, cancellationToken);

        return result.IsSuccess ? result.Value!.Id : 0;
    }

    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken)
    {
        var result = await userQueryService.Handle(new GetUserByEmailQuery(email), cancellationToken);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken)
    {
        var result = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return result?.Email ?? string.Empty;
    }

    public async Task<int> CreateUser(string username, string password, CancellationToken cancellationToken)
    {
        return await CreateUser(
            1,
            "User",
            "Generated",
            username,
            password,
            "DOCTOR",
            cancellationToken
        );
    }

    public async Task<int> FetchUserIdByUsername(string username, CancellationToken cancellationToken)
    {
        return await FetchUserIdByEmail(username, cancellationToken);
    }

    public async Task<string> FetchUsernameByUserId(int userId, CancellationToken cancellationToken)
    {
        return await FetchEmailByUserId(userId, cancellationToken);
    }
}