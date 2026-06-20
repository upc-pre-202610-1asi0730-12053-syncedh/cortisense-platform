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
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscription Endpoints.")]
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Subscriptions", "Get all subscriptions or filter by organizationId.")]
    public async Task<IActionResult> GetSubscriptions([FromQuery] int? organizationId, CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
            return Ok((await subscriptionQueryService.Handle(new GetSubscriptionByOrganizationIdQuery(organizationId.Value), cancellationToken)).Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));

        return Ok((await subscriptionQueryService.Handle(new GetAllSubscriptionsQuery(), cancellationToken)).Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation("Create Subscription", "Create a new subscription.")]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource, CancellationToken cancellationToken)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);
        
        return SubscriptionActionResultAssembler.ToActionResultFromCreateSubscriptionResult(
            this, result, problemDetailsFactory,
            subscription => CreatedAtAction(nameof(GetSubscriptions), new { organizationId = subscription.OrganizationId }, SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription))
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Subscription Plan", "Update a subscription to a new plan.")]
    public async Task<IActionResult> UpdateSubscription(int id, [FromBody] UpdateSubscriptionResource resource, CancellationToken cancellationToken)
    {
        var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);
        
        return SubscriptionActionResultAssembler.ToActionResultFromUpdateSubscriptionResult(
            this, result, problemDetailsFactory,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription))
        );
    }
}
