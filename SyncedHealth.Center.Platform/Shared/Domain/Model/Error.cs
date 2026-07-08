namespace SyncedHealth.Center.Platform.Shared.Domain.Model;

/// <summary>
/// Represents the error in the CortiSense Platform.
/// </summary>
public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
}