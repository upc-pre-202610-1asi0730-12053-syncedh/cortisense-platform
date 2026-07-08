using SyncedHealth.Center.Platform.Shared.Domain.Model;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Errors;

/// <summary>
/// Represents the iam errors in the CortiSense Platform.
/// </summary>
public static class IamErrors
{
    public static readonly Error InvalidCredentials = new("Iam.InvalidCredentials", "Invalid username or password.");

    public static readonly Error UsernameAlreadyTaken =
        new("Iam.UsernameAlreadyTaken", "The specified username is already taken.");

    public static readonly Error UserCreationFailed =
        new("Iam.UserCreationFailed", "An error occurred while creating the user.");
}