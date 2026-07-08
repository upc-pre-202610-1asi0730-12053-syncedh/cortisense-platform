using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
/// Represents the organization in the CortiSense Platform.
/// </summary>
public partial class Organization
{
    public Organization()
    {
        Name = string.Empty;
        Ruc = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Status = string.Empty;
        RegistrationStatus = string.Empty;
    }

    public Organization(CreateOrganizationCommand command)
    {
        Name = command.Name.Trim();
        Ruc = command.Ruc.Trim();
        Email = NormalizeEmail(command.Email);
        Phone = command.Phone.Trim();
        Address = command.Address.Trim();
        Status = NormalizeUpper(command.Status, "PENDING");
        RegistrationStatus = NormalizeUpper(command.RegistrationStatus, "PENDING");

        if (Status == "ACTIVE")
            ActivatedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Ruc { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string Address { get; private set; }

    public string Status { get; private set; }

    public string RegistrationStatus { get; private set; }

    public DateTimeOffset? ActivatedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public void Update(UpdateOrganizationCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Name))
            Name = command.Name.Trim();

        if (!string.IsNullOrWhiteSpace(command.Ruc))
            Ruc = command.Ruc.Trim();

        if (!string.IsNullOrWhiteSpace(command.Email))
            Email = NormalizeEmail(command.Email);

        if (!string.IsNullOrWhiteSpace(command.Phone))
            Phone = command.Phone.Trim();

        if (!string.IsNullOrWhiteSpace(command.Address))
            Address = command.Address.Trim();

        if (!string.IsNullOrWhiteSpace(command.Status))
            Status = command.Status.Trim().ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(command.RegistrationStatus))
            RegistrationStatus = command.RegistrationStatus.Trim().ToUpperInvariant();

        if (Status == "ACTIVE" && ActivatedAt is null)
            ActivatedAt = DateTimeOffset.UtcNow;

        if (Status is "CANCELLED" or "INACTIVE")
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