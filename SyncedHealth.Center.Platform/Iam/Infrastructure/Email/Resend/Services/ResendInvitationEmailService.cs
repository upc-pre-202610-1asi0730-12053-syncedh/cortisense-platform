using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SyncedHealth.Center.Platform.Iam.Application.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Configuration;
using SyncedHealth.Center.Platform.Iam.Resources;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Services;

public class ResendInvitationEmailService(
    HttpClient httpClient,
    IOptions<ResendSettings> resendSettingsOptions,
    IStringLocalizer<IamMessages> localizer)
    : IInvitationEmailService
{
    private readonly ResendSettings _settings = resendSettingsOptions.Value;
    private readonly IStringLocalizer<IamMessages> _localizer = localizer;

    public async Task<InvitationEmailResult> SendInvitationAsync(
        string email,
        string token,
        CancellationToken cancellationToken)
    {
        var apiToken = GetSettingValue(
            _settings.ApiToken,
            "RESEND_APITOKEN"
        );

        var fromEmail = GetSettingValue(
            _settings.FromEmail,
            "RESEND_FROM_EMAIL"
        );

        var frontendUrl = GetSettingValue(
            _settings.FrontendUrl,
            "APP_PUBLIC_URL"
        );

        if (string.IsNullOrWhiteSpace(apiToken))
        {
            return new InvitationEmailResult(
                false,
                "SKIPPED",
                null,
                null
            );
        }

        if (string.IsNullOrWhiteSpace(fromEmail) ||
            string.IsNullOrWhiteSpace(frontendUrl) ||
            string.IsNullOrWhiteSpace(_settings.ApiUrl))
        {
            return new InvitationEmailResult(
                false,
                "FAILED",
                null,
                _localizer["ResendConfigurationIncomplete"].Value
            );
        }

        var invitationUrl = BuildInvitationUrl(frontendUrl, token);

        var request = new ResendSendEmailRequest(
            fromEmail,
            [email],
            _localizer["InvitationEmailSubject"].Value,
            BuildInvitationEmailHtml(invitationUrl)
        );

        var json = JsonSerializer.Serialize(request);
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _settings.ApiUrl
        );

        httpRequest.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            apiToken
        );

        httpRequest.Content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json"
        );

        try
        {
            using var response = await httpClient.SendAsync(
                httpRequest,
                cancellationToken
            );

            var responseContent = await response.Content.ReadAsStringAsync(
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
            {
                return new InvitationEmailResult(
                    false,
                    "FAILED",
                    null,
                    $"{_localizer["ResendRequestFailed"].Value}: {responseContent}"
                );
            }

            var resendResponse = JsonSerializer.Deserialize<ResendSendEmailResponse>(
                responseContent
            );

            return new InvitationEmailResult(
                true,
                "SENT",
                resendResponse?.Id,
                null
            );
        }
        catch (Exception exception)
        {
            return new InvitationEmailResult(
                false,
                "FAILED",
                null,
                exception.Message
            );
        }
    }

    private string BuildInvitationEmailHtml(string invitationUrl)
    {
        return $$"""
        <div style="font-family: Arial, sans-serif; max-width: 560px; margin: 0 auto; padding: 24px;">
            <h2>{{_localizer["InvitationEmailTitle"].Value}}</h2>
            <p>{{_localizer["InvitationEmailIntro"].Value}}</p>

            <p style="margin: 32px 0;">
                <a href="{{invitationUrl}}"
                   style="background-color: #0891b2; color: #ffffff; padding: 12px 18px; border-radius: 8px; text-decoration: none; display: inline-block;">
                    {{_localizer["InvitationEmailButton"].Value}}
                </a>
            </p>

            <p>{{_localizer["InvitationEmailFallback"].Value}}</p>
            <p>
                <a href="{{invitationUrl}}">{{invitationUrl}}</a>
            </p>

            <p style="margin-top: 32px;">
                {{_localizer["InvitationEmailRegards"].Value}}
            </p>
        </div>
        """;
    }

    private static string BuildInvitationUrl(string frontendUrl, string token)
    {
        var cleanFrontendUrl = frontendUrl.TrimEnd('/');
        var encodedToken = Uri.EscapeDataString(token);

        return $"{cleanFrontendUrl}/accept-invitation?token={encodedToken}";
    }

    private static string GetSettingValue(string configuredValue, string environmentVariableName)
    {
        if (!string.IsNullOrWhiteSpace(configuredValue))
            return configuredValue;

        return Environment.GetEnvironmentVariable(environmentVariableName) ?? string.Empty;
    }

    private sealed record ResendSendEmailRequest(
        [property: JsonPropertyName("from")] string From,
        [property: JsonPropertyName("to")] string[] To,
        [property: JsonPropertyName("subject")] string Subject,
        [property: JsonPropertyName("html")] string Html
    );

    private sealed record ResendSendEmailResponse(
        [property: JsonPropertyName("id")] string? Id
    );
}