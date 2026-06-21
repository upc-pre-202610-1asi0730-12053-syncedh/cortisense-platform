using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.AuditCompliance.Application.CommandServices;
using SyncedHealth.Center.Platform.AuditCompliance.Application.QueryServices;
using SyncedHealth.Center.Platform.AuditCompliance.Domain.Model.Queries;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.AuditCompliance.Interfaces.Rest.Controllers;

/// <summary>
/// REST controller for audit log operations.
/// </summary>
[ApiController]
[Route("api/v1/auditLogs")]
public class AuditLogsController(
    IAuditLogCommandService auditLogCommandService,
    IAuditLogQueryService auditLogQueryService) : ControllerBase
{
    /// <summary>
    /// Gets all audit logs or filters them by organizationId / actorUserId.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] int? organizationId,
        [FromQuery] int? actorUserId,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var byOrganizationQuery = new GetAuditLogsByOrganizationIdQuery(organizationId.Value);
            var byOrganizationLogs = await auditLogQueryService.Handle(
                byOrganizationQuery,
                cancellationToken);

            return Ok(byOrganizationLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (actorUserId.HasValue)
        {
            var byActorQuery = new GetAuditLogsByActorUserIdQuery(actorUserId.Value);
            var byActorLogs = await auditLogQueryService.Handle(
                byActorQuery,
                cancellationToken);

            return Ok(byActorLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var query = new GetAllAuditLogsQuery();
        var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

        return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>
    /// Gets an audit log by identifier.
    /// </summary>
    [HttpGet("{auditLogId:int}")]
    public async Task<IActionResult> GetAuditLogById(
        int auditLogId,
        CancellationToken cancellationToken)
    {
        var query = new GetAuditLogByIdQuery(auditLogId);
        var auditLog = await auditLogQueryService.Handle(query, cancellationToken);

        if (auditLog is null)
            return NotFound();

        var resource = AuditLogResourceFromEntityAssembler.ToResourceFromEntity(auditLog);

        return Ok(resource);
    }

    /// <summary>
    /// Gets audit logs by organization identifier.
    /// </summary>
    [HttpGet("organizations/{organizationId:int}")]
    public async Task<IActionResult> GetAuditLogsByOrganizationId(
        int organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetAuditLogsByOrganizationIdQuery(organizationId);
        var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

        return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>
    /// Gets audit logs by actor user identifier.
    /// </summary>
    [HttpGet("actors/{actorUserId:int}")]
    public async Task<IActionResult> GetAuditLogsByActorUserId(
        int actorUserId,
        CancellationToken cancellationToken)
    {
        var query = new GetAuditLogsByActorUserIdQuery(actorUserId);
        var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

        return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>
    /// Creates an audit log.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAuditLog(
        [FromBody] CreateAuditLogResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateAuditLogCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await auditLogCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Message);

        var auditLogResource = AuditLogResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetAuditLogById),
            new { auditLogId = auditLogResource.Id },
            auditLogResource);
    }
}