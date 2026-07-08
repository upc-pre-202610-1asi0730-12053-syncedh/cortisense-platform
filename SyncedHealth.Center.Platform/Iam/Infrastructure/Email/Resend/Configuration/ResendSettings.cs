namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Configuration;

/// <summary>
/// Represents the resend settings in the CortiSense Platform.
/// </summary>
public class ResendSettings
{
    public string ApiToken { get; set; } = string.Empty;

    public string FromEmail { get; set; } =
        "CortiSense <invitations@notifications.cortisense.space>";

    public string FrontendUrl { get; set; } =
        "https://app.cortisense.space";

    public string ApiUrl { get; set; } =
        "https://api.resend.com/emails";
}