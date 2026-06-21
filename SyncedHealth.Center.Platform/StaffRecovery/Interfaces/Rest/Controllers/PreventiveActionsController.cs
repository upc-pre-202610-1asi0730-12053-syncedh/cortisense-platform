using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Repositories;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/preventiveActions")]
public class PreventiveActionsController(
    IRecoveryPlanCommandService recoveryPlanCommandService,
    IRecoveryPlanQueryService recoveryPlanQueryService,
    IRecoveryPlanRepository recoveryPlanRepository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPreventiveActions(
        [FromQuery] int? organizationId,
        [FromQuery] int? supervisorId,
        [FromQuery] int? userId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var result = userId.HasValue
            ? await recoveryPlanQueryService.Handle(
                new GetRecoveryPlansByMedicalStaffIdQuery(userId.Value),
                cancellationToken)
            : !string.IsNullOrWhiteSpace(status)
                ? await recoveryPlanQueryService.Handle(
                    new GetRecoveryPlansByStatusQuery(status),
                    cancellationToken)
                : await recoveryPlanQueryService.Handle(
                    new GetAllRecoveryPlansQuery(),
                    cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                error = result.Error?.ToString(),
                message = result.Message
            });

        var resources = result.Value!
            .Select(plan => ToPreventiveActionResource(
                plan.Id,
                organizationId ?? 0,
                supervisorId ?? 0,
                plan.MedicalStaffId,
                GuessTypeFromDescription(plan.Description),
                plan.Status,
                plan.Description,
                plan.CreatedAt,
                null));

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPreventiveActionById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await recoveryPlanQueryService.Handle(
            new GetRecoveryPlanByIdQuery(id),
            cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new
            {
                error = result.Error?.ToString(),
                message = result.Message
            });

        var plan = result.Value!;

        return Ok(ToPreventiveActionResource(
            plan.Id,
            0,
            0,
            plan.MedicalStaffId,
            GuessTypeFromDescription(plan.Description),
            plan.Status,
            plan.Description,
            plan.CreatedAt,
            null));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePreventiveAction(
        [FromBody] CreatePreventiveActionResource resource,
        CancellationToken cancellationToken)
    {
        var command = new IssueRecoveryRecommendationCommand(
            resource.UserId,
            BuildRecoveryDescription(resource.Type, resource.Notes),
            SuggestedRestDaysFromType(resource.Type));

        var result = await recoveryPlanCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new
            {
                error = result.Error?.ToString(),
                message = result.Message
            });

        var plan = result.Value!;

        var response = ToPreventiveActionResource(
            plan.Id,
            resource.OrganizationId,
            resource.SupervisorId,
            plan.MedicalStaffId,
            resource.Type,
            plan.Status,
            resource.Notes,
            plan.CreatedAt,
            null);

        return CreatedAtAction(
            nameof(GetPreventiveActionById),
            new { id = response.Id },
            response);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdatePreventiveActionStatus(
        int id,
        [FromBody] UpdatePreventiveActionStatusResource resource,
        CancellationToken cancellationToken)
    {
        var recoveryPlan = await recoveryPlanRepository.FindByIdAsync(id, cancellationToken);

        if (recoveryPlan is null)
            return NotFound(new { message = "Preventive action not found." });

        recoveryPlan.UpdateStatus(resource.Status);
        recoveryPlanRepository.Update(recoveryPlan);

        await unitOfWork.CompleteAsync(cancellationToken);

        var response = ToPreventiveActionResource(
            recoveryPlan.Id,
            0,
            0,
            recoveryPlan.MedicalStaffId,
            GuessTypeFromDescription(recoveryPlan.Description),
            recoveryPlan.Status,
            recoveryPlan.Description,
            recoveryPlan.CreatedAt,
            resource.CompletedAt);

        return Ok(response);
    }

    private static PreventiveActionResource ToPreventiveActionResource(
        int id,
        int organizationId,
        int supervisorId,
        int userId,
        string type,
        string status,
        string notes,
        DateTimeOffset? createdAt,
        DateTimeOffset? completedAt)
    {
        return new PreventiveActionResource(
            id,
            organizationId,
            supervisorId,
            userId,
            type,
            status,
            notes,
            createdAt ?? DateTimeOffset.UtcNow,
            completedAt);
    }

    private static string BuildRecoveryDescription(string type, string notes)
    {
        var normalizedType = string.IsNullOrWhiteSpace(type)
            ? "REST"
            : type.Trim().ToUpperInvariant();

        return $"[{normalizedType}] {notes?.Trim()}";
    }

    private static string GuessTypeFromDescription(string description)
    {
        if (description.StartsWith("[") && description.Contains(']'))
            return description[1..description.IndexOf(']')];

        return "REST";
    }

    private static int SuggestedRestDaysFromType(string type)
    {
        var normalizedType = string.IsNullOrWhiteSpace(type)
            ? "REST"
            : type.Trim().ToUpperInvariant();

        return normalizedType switch
        {
            "MEDICAL_EVALUATION" => 2,
            "SHIFT_REASSIGNMENT" => 1,
            "BREAK" => 1,
            "REST" => 1,
            _ => 1
        };
    }
}

public record CreatePreventiveActionResource(
    int OrganizationId,
    int SupervisorId,
    int UserId,
    string Type,
    string Notes
);

public record UpdatePreventiveActionStatusResource(
    string Status,
    DateTimeOffset? CompletedAt
);

public record PreventiveActionResource(
    int Id,
    int OrganizationId,
    int SupervisorId,
    int UserId,
    string Type,
    string Status,
    string Notes,
    DateTimeOffset CreatedAt,
    DateTimeOffset? CompletedAt
);