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
    [HttpGet]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] int? organizationId,
        [FromQuery] int? actorUserId,
        [FromQuery] int? medicalStaffId,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var query = new GetAuditLogsByOrganizationIdQuery(organizationId.Value);
            var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

            return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
        }

        // medicalStaffId is a semantic alias for actorUserId (medical staff perform actions)
        var resolvedActorUserId = actorUserId ?? medicalStaffId;

        if (resolvedActorUserId.HasValue)
        {
            var query = new GetAuditLogsByActorUserIdQuery(resolvedActorUserId.Value);
            var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

            return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var allQuery = new GetAllAuditLogsQuery();
        var allAuditLogs = await auditLogQueryService.Handle(allQuery, cancellationToken);

        return Ok(allAuditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

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

    [HttpGet("organizations/{organizationId:int}")]
    public async Task<IActionResult> GetAuditLogsByOrganizationId(
        int organizationId,
        CancellationToken cancellationToken)
    {
        var query = new GetAuditLogsByOrganizationIdQuery(organizationId);
        var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

        return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("actors/{actorUserId:int}")]
    public async Task<IActionResult> GetAuditLogsByActorUserId(
        int actorUserId,
        CancellationToken cancellationToken)
    {
        var query = new GetAuditLogsByActorUserIdQuery(actorUserId);
        var auditLogs = await auditLogQueryService.Handle(query, cancellationToken);

        return Ok(auditLogs.Select(AuditLogResourceFromEntityAssembler.ToResourceFromEntity));
    }

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