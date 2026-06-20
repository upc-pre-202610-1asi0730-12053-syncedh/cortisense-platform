using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.CommandServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Application.QueryServices;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest.Transform;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace SyncedHealth.Center.Platform.ClinicalRiskAssessment.Interfaces.Rest;

[ApiController]
[Route("api/v1/clinicalAlerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Clinical Alert Endpoints.")]
public class ClinicalAlertsController(
    IClinicalAlertCommandService clinicalAlertCommandService,
    IClinicalAlertQueryService clinicalAlertQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Clinical Alerts", "Get all clinical alerts or filter by organizationId/userId/status.")]
    public async Task<IActionResult> GetClinicalAlerts(
        [FromQuery] int? organizationId,
        [FromQuery] int? userId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await clinicalAlertQueryService.Handle(
                new GetClinicalAlertsByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var result = await clinicalAlertQueryService.Handle(
                new GetClinicalAlertsByUserIdQuery(userId.Value),
                cancellationToken);

            return Ok(result.Select(ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            var result = await clinicalAlertQueryService.Handle(
                new GetClinicalAlertsByStatusQuery(status),
                cancellationToken);

            return Ok(result.Select(ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var clinicalAlerts = await clinicalAlertQueryService.Handle(
            new GetAllClinicalAlertsQuery(),
            cancellationToken);

        return Ok(clinicalAlerts.Select(ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Clinical Alert by Id", "Get a clinical alert by its identifier.")]
    public async Task<IActionResult> GetClinicalAlertById(int id, CancellationToken cancellationToken)
    {
        var clinicalAlert = await clinicalAlertQueryService.Handle(
            new GetClinicalAlertByIdQuery(id),
            cancellationToken);

        if (clinicalAlert is null)
            return NotFound();

        return Ok(ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity(clinicalAlert));
    }

    [HttpPost]
    [SwaggerOperation("Create Clinical Alert", "Create a new clinical alert.")]
    public async Task<IActionResult> CreateClinicalAlert(
        [FromBody] CreateClinicalAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateClinicalAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await clinicalAlertCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromCreateClinicalAlertResult(
            this,
            result,
            problemDetailsFactory,
            createdClinicalAlert => CreatedAtAction(
                nameof(GetClinicalAlertById),
                new { id = createdClinicalAlert.Id },
                ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity(createdClinicalAlert)
            )
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Clinical Alert Status", "Update the status of a clinical alert.")]
    public async Task<IActionResult> UpdateClinicalAlertStatus(
        int id,
        [FromBody] UpdateClinicalAlertStatusResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateClinicalAlertStatusCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await clinicalAlertCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromUpdateClinicalAlertStatusResult(
            this,
            result,
            problemDetailsFactory,
            updatedClinicalAlert => Ok(
                ClinicalAlertResourceFromEntityAssembler.ToResourceFromEntity(updatedClinicalAlert)
            )
        );
    }
}