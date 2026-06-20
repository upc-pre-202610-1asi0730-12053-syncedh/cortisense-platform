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
[Route("api/v1/vitalSignReadings")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Vital Sign Reading Endpoints.")]
public class VitalSignReadingsController(
    IVitalSignReadingCommandService vitalSignReadingCommandService,
    IVitalSignReadingQueryService vitalSignReadingQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Vital Sign Readings", "Get all vital sign readings or filter by organizationId/userId.")]
    public async Task<IActionResult> GetVitalSignReadings(
        [FromQuery] int? organizationId,
        [FromQuery] int? userId,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await vitalSignReadingQueryService.Handle(
                new GetVitalSignReadingsByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(VitalSignReadingResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var result = await vitalSignReadingQueryService.Handle(
                new GetVitalSignReadingsByUserIdQuery(userId.Value),
                cancellationToken);

            return Ok(result.Select(VitalSignReadingResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var readings = await vitalSignReadingQueryService.Handle(
            new GetAllVitalSignReadingsQuery(),
            cancellationToken);

        return Ok(readings.Select(VitalSignReadingResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Vital Sign Reading by Id", "Get a vital sign reading by its identifier.")]
    public async Task<IActionResult> GetVitalSignReadingById(int id, CancellationToken cancellationToken)
    {
        var reading = await vitalSignReadingQueryService.Handle(
            new GetVitalSignReadingByIdQuery(id),
            cancellationToken);

        if (reading is null)
            return NotFound();

        return Ok(VitalSignReadingResourceFromEntityAssembler.ToResourceFromEntity(reading));
    }

    [HttpPost]
    [SwaggerOperation("Create Vital Sign Reading", "Create a new vital sign reading.")]
    public async Task<IActionResult> CreateVitalSignReading(
        [FromBody] CreateVitalSignReadingResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateVitalSignReadingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await vitalSignReadingCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromCreateVitalSignReadingResult(
            this,
            result,
            problemDetailsFactory,
            createdReading => CreatedAtAction(
                nameof(GetVitalSignReadingById),
                new { id = createdReading.Id },
                VitalSignReadingResourceFromEntityAssembler.ToResourceFromEntity(createdReading)
            )
        );
    }
}