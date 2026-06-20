using System.ComponentModel.DataAnnotations;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

/// <summary>
/// Resource used to create an audit log.
/// </summary>
public record CreateAuditLogResource(
    [Required]
    [Range(1, int.MaxValue)]
    int OrganizationId,

    [Required]
    [Range(1, int.MaxValue)]
    int ActorUserId,

    [Required]
    EAuditLogType Type,

    [Required]
    EAuditSeverity Severity,

    [Required]
    EAuditResourceType ResourceType,

    [Required]
    [Range(1, int.MaxValue)]
    int ResourceId,

    [Required]
    EAuditActionSource Source,

    [Required]
    [StringLength(500, MinimumLength = 5)]
    string Description
);