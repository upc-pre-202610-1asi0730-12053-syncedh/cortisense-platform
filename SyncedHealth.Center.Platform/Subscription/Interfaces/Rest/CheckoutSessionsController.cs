using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.Subscription.Application.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest;

[ApiController]
[Route("api/v1/checkoutSessions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Checkout Session Endpoints.")]
public class CheckoutSessionsController(
    ICheckoutSessionCommandService checkoutSessionCommandService,
    ICheckoutSessionQueryService checkoutSessionQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Checkout Sessions", "Get all checkout sessions for an organization.")]
    public async Task<IActionResult> GetCheckoutSessions([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var sessions = await checkoutSessionQueryService.Handle(new GetCheckoutSessionsByOrganizationIdQuery(organizationId), cancellationToken);
        return Ok(sessions.Select(CheckoutSessionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation("Create Checkout Session", "Create a new checkout session.")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionResource resource, CancellationToken cancellationToken)
    {
        var command = CreateCheckoutSessionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await checkoutSessionCommandService.Handle(command, cancellationToken);
        
        return SubscriptionActionResultAssembler.ToActionResultFromCreateCheckoutSessionResult(
            this, result, problemDetailsFactory,
            session => CreatedAtAction(nameof(GetCheckoutSessions), new { organizationId = session.OrganizationId }, CheckoutSessionResourceFromEntityAssembler.ToResourceFromEntity(session))
        );
    }
}
