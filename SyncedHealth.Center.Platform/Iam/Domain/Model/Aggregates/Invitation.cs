using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

public partial class Invitation
{
    public Invitation()
    {
        Email = string.Empty;
        Role = string.Empty;
        Status = string.Empty;
        Token = string.Empty;
        EmailStatus = string.Empty;
    }

    public Invitation(CreateInvitationCommand command)
    {
        OrganizationId = command.OrganizationId;
        Email = NormalizeEmail(command.Email);
        Role = NormalizeUpper(command.Role, "DOCTOR");
        Status = NormalizeUpper(command.Status, "PENDING");
        Token = string.IsNullOrWhiteSpace(command.Token)
            ? Guid.NewGuid().ToString("N")
            : command.Token.Trim();
        EmailStatus = "PENDING";
        ExpiresAt = command.ExpiresAt ?? DateTimeOffset.UtcNow.AddDays(7);
    }

    public int Id { get; private set; }

    public int OrganizationId { get; private set; }

    public string Email { get; private set; }

    public string Role { get; private set; }

    public string Status { get; private set; }

    public string Token { get; private set; }

    public string EmailStatus { get; private set; }

    public string? ResendEmailId { get; private set; }

    public string? EmailError { get; private set; }

    public DateTimeOffset? ExpiresAt { get; private set; }

    public DateTimeOffset? SentAt { get; private set; }

    public DateTimeOffset? AcceptedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public void Update(UpdateInvitationCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Email))
            Email = NormalizeEmail(command.Email);

        if (!string.IsNullOrWhiteSpace(command.Role))
            Role = command.Role.Trim().ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(command.Status))
        {
            Status = command.Status.Trim().ToUpperInvariant();

            if (Status == "ACCEPTED" && AcceptedAt is null)
                AcceptedAt = DateTimeOffset.UtcNow;

            if (Status == "CANCELLED" && CancelledAt is null)
                CancelledAt = DateTimeOffset.UtcNow;
        }

        if (!string.IsNullOrWhiteSpace(command.EmailStatus))
        {
            EmailStatus = command.EmailStatus.Trim().ToUpperInvariant();

            if (EmailStatus == "SENT" && SentAt is null)
                SentAt = DateTimeOffset.UtcNow;
        }

        if (!string.IsNullOrWhiteSpace(command.ResendEmailId))
            ResendEmailId = command.ResendEmailId.Trim();

        if (!string.IsNullOrWhiteSpace(command.EmailError))
            EmailError = command.EmailError.Trim();

        if (command.ExpiresAt.HasValue)
            ExpiresAt = command.ExpiresAt;
    }

    public void MarkEmailAsSent(string resendEmailId)
    {
        EmailStatus = "SENT";
        ResendEmailId = resendEmailId;
        EmailError = null;
        SentAt = DateTimeOffset.UtcNow;
        Status = "SENT";
    }

    public void MarkEmailAsSkipped()
    {
        EmailStatus = "SKIPPED";
        EmailError = null;
    }

    public void MarkEmailAsFailed(string error)
    {
        EmailStatus = "FAILED";
        EmailError = error;
    }

    public void MarkAsAccepted()
    {
        Status = "ACCEPTED";
        AcceptedAt = DateTimeOffset.UtcNow;
    }

    public void Cancel()
    {
        Status = "CANCELLED";
        CancelledAt = DateTimeOffset.UtcNow;
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static string NormalizeUpper(string? value, string fallback)
    {
        return string.IsNullOrWhiteSpace(value)
            ? fallback
            : value.Trim().ToUpperInvariant();
    }
}