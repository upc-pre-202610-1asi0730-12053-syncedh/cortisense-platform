using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SyncedHealth.Center.Platform.Iam.Application.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Configuration;
using SyncedHealth.Center.Platform.Iam.Resources;
using IamMessagesResource = SyncedHealth.Center.Platform.Iam.Resources.IamMessages;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Email.Resend.Services;

/// <summary>
/// Represents the resend invitation email service in the CortiSense Platform.
/// </summary>
public class ResendInvitationEmailService(
    HttpClient httpClient,
    IOptions<ResendSettings> resendSettingsOptions,
    IStringLocalizer<IamMessagesResource> localizer)
    : IInvitationEmailService
{
    private readonly ResendSettings _settings = resendSettingsOptions.Value;
    private readonly IStringLocalizer<IamMessagesResource> _localizer = localizer;

    public async Task<InvitationEmailResult> SendInvitationAsync(
        string email,
        string token,
        string role,
        string organizationName,
        CancellationToken cancellationToken)
    {
        var apiToken = GetSettingValue(_settings.ApiToken, "RESEND_APITOKEN");
        var fromEmail = GetSettingValue(_settings.FromEmail, "RESEND_FROM_EMAIL");
        var frontendUrl = GetSettingValue(_settings.FrontendUrl, "APP_PUBLIC_URL");

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
                Localize("ResendConfigurationIncomplete", "La configuración de Resend está incompleta.")
            );
        }

        var invitationUrl = BuildInvitationUrl(frontendUrl, token);
        var roleLabel = ResolveRoleLabel(role);
        var safeOrganizationName = string.IsNullOrWhiteSpace(organizationName)
            ? Localize("InvitationDefaultOrganizationName", "tu centro médico")
            : organizationName.Trim();

        var request = new ResendSendEmailRequest(
            fromEmail,
            [email],
            Localize("InvitationEmailSubject", "Has sido invitado a CortiSense"),
            BuildInvitationEmailHtml(
                email,
                roleLabel,
                safeOrganizationName,
                invitationUrl
            )
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
                    $"{Localize("ResendRequestFailed", "La solicitud a Resend falló.")}: {responseContent}"
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

    private string BuildInvitationEmailHtml(
        string email,
        string roleLabel,
        string organizationName,
        string invitationUrl)
    {
        return $$"""
        <!doctype html>
        <html lang="es">
          <head>
            <meta charset="UTF-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1.0" />
            <title>{{Localize("InvitationEmailSubject", "Has sido invitado a CortiSense")}}</title>
          </head>

          <body style="margin:0; padding:0; background:#ecfeff; font-family:Arial, Helvetica, sans-serif;">
            <table width="100%" cellpadding="0" cellspacing="0" role="presentation" style="background:#ecfeff; padding:48px 16px;">
              <tr>
                <td align="center">
                  <table width="100%" cellpadding="0" cellspacing="0" role="presentation"
                         style="max-width:820px; background:#ffffff; border-radius:28px; padding:48px 44px 42px; box-shadow:0 18px 45px rgba(15,23,42,0.08);">

                    <tr>
                      <td align="center" style="padding-bottom:28px;">
                        <table cellpadding="0" cellspacing="0" role="presentation" align="center" style="margin:0 auto;">
                          <tr>
                            <td align="center" valign="middle" style="padding-right:14px;">
                              <div style="width:58px; height:58px; border-radius:16px; border:2px solid #45d6e3; background:#f0fdff; color:#20c7d4; font-size:26px; font-weight:800; line-height:58px; text-align:center;">
                                +
                              </div>
                            </td>

                            <td align="left" valign="middle">
                              <h1 style="margin:0; color:#45d6e3; font-size:46px; line-height:1.1; font-weight:800; font-style:italic;">
                                CortiSense
                              </h1>
                            </td>
                          </tr>
                        </table>

                        <p style="margin:12px 0 0; color:#64748b; font-size:16px; line-height:1.5;">
                          {{Localize("InvitationEmailSubtitle", "Sistema clínico preventivo para equipos de salud")}}
                        </p>
                      </td>
                    </tr>

                    <tr>
                      <td align="center" style="padding:0 20px;">
                        <p style="margin:0 0 24px; color:#050b18; font-size:25px; line-height:1.45;">
                          {{Localize("InvitationEmailGreeting", "Hola, fuiste invitado a formar parte como")}}
                          <strong style="color:#0eabb8; font-style:italic;">{{roleLabel}}</strong>
                          {{Localize("InvitationEmailOrganizationPrefix", "en el centro médico")}}
                          <strong style="color:#0eabb8; font-style:italic;">{{organizationName}}</strong>.
                        </p>

                        <p style="margin:0 0 24px; color:#050b18; font-size:24px; line-height:1.45;">
                          {{Localize("InvitationEmailEmailPrefix", "Se utilizará el correo")}}
                          <strong style="color:#0eabb8; font-style:italic;">{{email}}</strong>
                          {{Localize("InvitationEmailEmailSuffix", "para poder registrarte.")}}
                        </p>

                        <p style="margin:0 0 32px; color:#050b18; font-size:24px; line-height:1.45;">
                          {{Localize("InvitationEmailInstruction", "¡Dale click al siguiente botón para poder iniciar tu registro!")}}
                        </p>

                        <a href="{{invitationUrl}}"
                           style="display:inline-block; padding:18px 32px; border-radius:12px; background:#45d6e3; color:#ffffff; text-decoration:none; font-size:24px; font-weight:800; box-shadow:0 14px 28px rgba(69,214,227,0.28);">
                          {{Localize("InvitationEmailButton", "Crear mi cuenta")}}
                        </a>

                        <p style="margin:42px 0 16px; color:#050b18; font-size:24px; line-height:1.45;">
                          {{Localize("InvitationEmailFallback", "Si el botón no funciona, copia y pega este enlace en tu navegador:")}}
                        </p>

                        <a href="{{invitationUrl}}"
                           style="display:block; color:#0eabb8; font-size:22px; line-height:1.5; font-weight:800; font-style:italic; text-decoration:none; word-break:break-all;">
                          {{invitationUrl}}
                        </a>
                      </td>
                    </tr>

                  </table>
                </td>
              </tr>
            </table>
          </body>
        </html>
        """;
    }

    private string ResolveRoleLabel(string role)
    {
        return role.Trim().ToUpperInvariant() switch
        {
            "HOSPITAL_ADMIN" or "ADMIN" => Localize(
                "InvitationRoleHospitalAdmin",
                "Administrador hospitalario"
            ),
            "SUPERVISOR" or "CLINICAL_SUPERVISOR" => Localize(
                "InvitationRoleSupervisor",
                "Supervisor clínico"
            ),
            "DOCTOR" => Localize(
                "InvitationRoleDoctor",
                "Doctor"
            ),
            "MEDICAL_STAFF" => Localize(
                "InvitationRoleMedicalStaff",
                "Personal médico"
            ),
            _ => role
        };
    }

    private string Localize(string key, string fallback)
    {
        var value = _localizer[key];

        return value.ResourceNotFound || string.IsNullOrWhiteSpace(value.Value)
            ? fallback
            : value.Value;
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

    /// <summary>
    /// Represents the resend send email request in the CortiSense Platform.
    /// </summary>
    private sealed record ResendSendEmailRequest(
        [property: JsonPropertyName("from")] string From,
        [property: JsonPropertyName("to")] string[] To,
        [property: JsonPropertyName("subject")] string Subject,
        [property: JsonPropertyName("html")] string Html
    );

    /// <summary>
    /// Represents the resend send email response in the CortiSense Platform.
    /// </summary>
    private sealed record ResendSendEmailResponse(
        [property: JsonPropertyName("id")] string? Id
    );
}