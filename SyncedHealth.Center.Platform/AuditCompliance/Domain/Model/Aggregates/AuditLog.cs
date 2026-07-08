using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Commands;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Aggregates;

/// <summary>
/// Represents the audit log in the CortiSense Platform.
/// </summary>
public partial class AuditLog
{
    public AuditLog()
    {
        Description = string.Empty;
    }

    public AuditLog(CreateAuditLogCommand command)
    {
        ValidateCommand(command);

        OrganizationId = command.OrganizationId;
        ActorUserId = command.ActorUserId;
        Type = command.Type;
        Severity = command.Severity;
        ResourceType = command.ResourceType;
        ResourceId = command.ResourceId;
        Source = command.Source;
        Description = command.Description.Trim();
    }

    public int Id { get; private set; }

    public int OrganizationId { get; private set; }

    public int ActorUserId { get; private set; }

    public EAuditLogType Type { get; private set; }

    public EAuditSeverity Severity { get; private set; }

    public EAuditResourceType ResourceType { get; private set; }

    public int ResourceId { get; private set; }

    public EAuditActionSource Source { get; private set; }

    public string Description { get; private set; }

    private static void ValidateCommand(CreateAuditLogCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (command.OrganizationId <= 0)
            throw new ArgumentException("OrganizationId must be greater than zero.", nameof(command));

        if (command.ActorUserId <= 0)
            throw new ArgumentException("ActorUserId must be greater than zero.", nameof(command));

        if (command.ResourceId <= 0)
            throw new ArgumentException("ResourceId must be greater than zero.", nameof(command));

        if (string.IsNullOrWhiteSpace(command.Description))
            throw new ArgumentException("Description cannot be empty.", nameof(command));
    }
}