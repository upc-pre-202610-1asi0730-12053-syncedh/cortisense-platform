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
[Route("api/v1/vitalSignAnomalies")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Vital Sign Anomaly Endpoints.")]
/// <summary>
/// Controller for vital sign anomalies REST API endpoints.
/// </summary>
public class VitalSignAnomaliesController(
    IVitalSignAnomalyCommandService vitalSignAnomalyCommandService,
    IVitalSignAnomalyQueryService vitalSignAnomalyQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Vital Sign Anomalies", "Get all vital sign anomalies or filter by organizationId/userId/status.")]
    public async Task<IActionResult> GetVitalSignAnomalies(
        [FromQuery] int? organizationId,
        [FromQuery] int? userId,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await vitalSignAnomalyQueryService.Handle(
                new GetVitalSignAnomaliesByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var result = await vitalSignAnomalyQueryService.Handle(
                new GetVitalSignAnomaliesByUserIdQuery(userId.Value),
                cancellationToken);

            return Ok(result.Select(VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            var result = await vitalSignAnomalyQueryService.Handle(
                new GetVitalSignAnomaliesByStatusQuery(status),
                cancellationToken);

            return Ok(result.Select(VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var anomalies = await vitalSignAnomalyQueryService.Handle(
            new GetAllVitalSignAnomaliesQuery(),
            cancellationToken);

        return Ok(anomalies.Select(VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Vital Sign Anomaly by Id", "Get a vital sign anomaly by its identifier.")]
    public async Task<IActionResult> GetVitalSignAnomalyById(int id, CancellationToken cancellationToken)
    {
        var anomaly = await vitalSignAnomalyQueryService.Handle(
            new GetVitalSignAnomalyByIdQuery(id),
            cancellationToken);

        if (anomaly is null)
            return NotFound();

        return Ok(VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity(anomaly));
    }

    [HttpPost]
    [SwaggerOperation("Create Vital Sign Anomaly", "Create a new vital sign anomaly.")]
    public async Task<IActionResult> CreateVitalSignAnomaly(
        [FromBody] CreateVitalSignAnomalyResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateVitalSignAnomalyCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await vitalSignAnomalyCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromCreateVitalSignAnomalyResult(
            this,
            result,
            problemDetailsFactory,
            createdAnomaly => CreatedAtAction(
                nameof(GetVitalSignAnomalyById),
                new { id = createdAnomaly.Id },
                VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity(createdAnomaly)
            )
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Vital Sign Anomaly Status", "Update the status of a vital sign anomaly.")]
    public async Task<IActionResult> UpdateVitalSignAnomalyStatus(
        int id,
        [FromBody] UpdateVitalSignAnomalyStatusResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateVitalSignAnomalyStatusCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await vitalSignAnomalyCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromUpdateVitalSignAnomalyStatusResult(
            this,
            result,
            problemDetailsFactory,
            updatedAnomaly => Ok(
                VitalSignAnomalyResourceFromEntityAssembler.ToResourceFromEntity(updatedAnomaly)
            )
        );
    }
}