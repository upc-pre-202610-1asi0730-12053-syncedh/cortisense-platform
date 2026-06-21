using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest;

[ApiController]
[Route("api/v1/plans")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Plan Endpoints.")]
public class PlansController(IPlanQueryService planQueryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation("Get All Plans", "Get all subscription plans.")]
    public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery(), cancellationToken);
        return Ok(plans.Select(PlanResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Plan by Id", "Get a subscription plan by its identifier.")]
    public async Task<IActionResult> GetPlanById(int id, CancellationToken cancellationToken)
    {
        var plan = await planQueryService.Handle(new GetPlanByIdQuery(id), cancellationToken);

        if (plan is null)
            return NotFound();

        return Ok(PlanResourceFromEntityAssembler.ToResourceFromEntity(plan));
    }
}