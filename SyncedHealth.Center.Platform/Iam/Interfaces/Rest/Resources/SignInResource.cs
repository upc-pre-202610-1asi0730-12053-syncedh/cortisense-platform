namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
/// Represents the sign in resource in the CortiSense Platform.
/// </summary>
public record SignInResource(
    string Email,
    string Password
);