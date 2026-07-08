namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

/// <summary>
/// Command to sign in.
/// </summary>
public record SignInCommand(
    string Email,
    string Password
);