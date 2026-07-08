namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

/**
 * <summary>
 *     This class is used to store the token settings.
 *     It is used to configure the token settings in the app settings .json file.
 * </summary>
 */
/// <summary>
/// Represents the token settings in the CortiSense Platform.
/// </summary>
public class TokenSettings
{
    public required string Secret { get; set; }
}