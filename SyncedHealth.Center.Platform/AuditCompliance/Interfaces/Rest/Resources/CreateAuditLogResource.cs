using System.ComponentModel.DataAnnotations;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;

/// <summary>
/// Represents the create audit log resource in the CortiSense Platform.
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