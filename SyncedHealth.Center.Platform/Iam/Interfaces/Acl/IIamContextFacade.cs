namespace SyncedHealth.Center.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> CreateUser(
        int organizationId,
        string firstName,
        string lastName,
        string email,
        string password,
        string role,
        CancellationToken cancellationToken
    );

    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken);

    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken);

    // Temporary compatibility with old method names.
    Task<int> CreateUser(string username, string password, CancellationToken cancellationToken);

    Task<int> FetchUserIdByUsername(string username, CancellationToken cancellationToken);

    Task<string> FetchUsernameByUserId(int userId, CancellationToken cancellationToken);
}