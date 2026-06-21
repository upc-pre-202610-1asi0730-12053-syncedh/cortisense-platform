using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Application.QueryServices;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Queries;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/recovery-plans")]
public class RecoveryPlansController(
    IRecoveryPlanCommandService recoveryPlanCommandService,
    IRecoveryPlanQueryService recoveryPlanQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecoveryPlans(
        [FromQuery] int? medicalStaffId,
        [FromQuery] string? status,
        [FromQuery] int? suggestedRestDays,
        CancellationToken cancellationToken)
    {
        var result = medicalStaffId.HasValue
            ? await recoveryPlanQueryService.Handle(
                new GetRecoveryPlansByMedicalStaffIdQuery(medicalStaffId.Value),
                cancellationToken)
            : !string.IsNullOrWhiteSpace(status)
                ? await recoveryPlanQueryService.Handle(
                    new GetRecoveryPlansByStatusQuery(status),
                    cancellationToken)
                : suggestedRestDays.HasValue
                    ? await recoveryPlanQueryService.Handle(
                        new GetRecoveryPlansBySuggestedRestDaysQuery(suggestedRestDays.Value),
                        cancellationToken)
                    : await recoveryPlanQueryService.Handle(
                        new GetAllRecoveryPlansQuery(),
                        cancellationToken);

        if (!result.IsSuccess)
            return StaffRecoveryActionResultAssembler.ToActionResult(result);

        var resources = RecoveryPlanResourceFromEntityAssembler
            .ToResourceFromEntityCollection(result.Value!);

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRecoveryPlanById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await recoveryPlanQueryService.Handle(
            new GetRecoveryPlanByIdQuery(id),
            cancellationToken);

        if (!result.IsSuccess)
            return StaffRecoveryActionResultAssembler.ToActionResult(result);

        var resource = RecoveryPlanResourceFromEntityAssembler
            .ToResourceFromEntity(result.Value!);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> IssueRecoveryRecommendation(
        [FromBody] IssueRecoveryRecommendationResource resource,
        CancellationToken cancellationToken)
    {
        var command = IssueRecoveryRecommendationCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        var result = await recoveryPlanCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return StaffRecoveryActionResultAssembler.ToActionResult(result);

        var recoveryPlanResource = RecoveryPlanResourceFromEntityAssembler
            .ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetRecoveryPlanById),
            new { id = recoveryPlanResource.Id },
            recoveryPlanResource);
    }
}