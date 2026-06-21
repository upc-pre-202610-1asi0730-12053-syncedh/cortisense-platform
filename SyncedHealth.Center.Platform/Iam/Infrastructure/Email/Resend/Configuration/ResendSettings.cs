namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Configuration;

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