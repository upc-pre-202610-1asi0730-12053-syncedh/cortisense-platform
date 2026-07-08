using System.Text.Json.Serialization;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
/// Represents the user in the CortiSense Platform.
/// </summary>
public partial class User
{
    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
        Role = string.Empty;
        Status = string.Empty;
        RegistrationStatus = string.Empty;
    }

    public User(SignUpCommand command, string passwordHash)
    {
        OrganizationId = command.OrganizationId;
        FirstName = command.FirstName.Trim();
        LastName = command.LastName.Trim();
        Email = NormalizeEmail(command.Email);
        PasswordHash = passwordHash;
        Phone = command.Phone;
        WorkAreaId = command.WorkAreaId;
        SpecialtyId = command.SpecialtyId;
        Role = NormalizeUpper(command.Role, "DOCTOR");
        Status = NormalizeUpper(command.Status, "ACTIVE");
        RegistrationStatus = NormalizeUpper(command.RegistrationStatus, "COMPLETED");
    }

    public int Id { get; private set; }

    public int OrganizationId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; private set; }

    [JsonIgnore]
    public string PasswordHash { get; private set; }

    public string? Phone { get; private set; }

    public int? WorkAreaId { get; private set; }

    public int? SpecialtyId { get; private set; }

    public string Role { get; private set; }

    public string Status { get; private set; }

    public string RegistrationStatus { get; private set; }

    public DateTimeOffset? ActivatedAt { get; private set; }

    public DateTimeOffset? CancelledAt { get; private set; }

    public void Update(UpdateUserCommand command, string? passwordHash = null)
    {
        if (!string.IsNullOrWhiteSpace(command.FirstName))
            FirstName = command.FirstName.Trim();

        if (!string.IsNullOrWhiteSpace(command.LastName))
            LastName = command.LastName.Trim();

        if (!string.IsNullOrWhiteSpace(command.Email))
            Email = NormalizeEmail(command.Email);

        if (!string.IsNullOrWhiteSpace(command.Phone))
            Phone = command.Phone.Trim();

        if (command.WorkAreaId.HasValue)
            WorkAreaId = command.WorkAreaId;

        if (command.SpecialtyId.HasValue)
            SpecialtyId = command.SpecialtyId;

        if (!string.IsNullOrWhiteSpace(command.Role))
            Role = command.Role.ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(command.Status))
            Status = command.Status.ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(command.RegistrationStatus))
            RegistrationStatus = command.RegistrationStatus.ToUpperInvariant();

        if (!string.IsNullOrWhiteSpace(passwordHash))
            PasswordHash = passwordHash;

        if (Status == "ACTIVE" && ActivatedAt is null)
            ActivatedAt = DateTimeOffset.UtcNow;

        if (Status is "INACTIVE" or "CANCELLED")
            CancelledAt = DateTimeOffset.UtcNow;
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
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