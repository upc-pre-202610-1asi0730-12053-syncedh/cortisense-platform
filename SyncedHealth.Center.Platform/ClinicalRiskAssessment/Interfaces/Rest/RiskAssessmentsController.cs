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
[Route("api/v1/riskAssessments")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Risk Assessment Endpoints.")]
/// <summary>
/// Controller for risk assessments REST API endpoints.
/// </summary>
public class RiskAssessmentsController(
    IRiskAssessmentCommandService riskAssessmentCommandService,
    IRiskAssessmentQueryService riskAssessmentQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Risk Assessments", "Get all risk assessments or filter by organizationId/userId.")]
    public async Task<IActionResult> GetRiskAssessments(
        [FromQuery] int? organizationId,
        [FromQuery] int? userId,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await riskAssessmentQueryService.Handle(
                new GetRiskAssessmentsByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var result = await riskAssessmentQueryService.Handle(
                new GetRiskAssessmentsByUserIdQuery(userId.Value),
                cancellationToken);

            return Ok(result.Select(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var riskAssessments = await riskAssessmentQueryService.Handle(
            new GetAllRiskAssessmentsQuery(),
            cancellationToken);

        return Ok(riskAssessments.Select(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Risk Assessment by Id", "Get a risk assessment by its identifier.")]
    public async Task<IActionResult> GetRiskAssessmentById(int id, CancellationToken cancellationToken)
    {
        var riskAssessment = await riskAssessmentQueryService.Handle(
            new GetRiskAssessmentByIdQuery(id),
            cancellationToken);

        if (riskAssessment is null)
            return NotFound();

        return Ok(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity(riskAssessment));
    }

    [HttpPost]
    [SwaggerOperation("Create Risk Assessment", "Create a new risk assessment.")]
    public async Task<IActionResult> CreateRiskAssessment(
        [FromBody] CreateRiskAssessmentResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateRiskAssessmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await riskAssessmentCommandService.Handle(command, cancellationToken);

        return ClinicalRiskAssessmentActionResultAssembler.ToActionResultFromCreateRiskAssessmentResult(
            this,
            result,
            problemDetailsFactory,
            createdRiskAssessment => CreatedAtAction(
                nameof(GetRiskAssessmentById),
                new { id = createdRiskAssessment.Id },
                RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity(createdRiskAssessment)
            )
        );
    }
}