using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.Subscription.Application.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Application.QueryServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest;

[ApiController]
[Route("api/v1/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Subscription Endpoints.")]
/// <summary>
/// Controller for subscriptions REST API endpoints.
/// </summary>
public class SubscriptionsController(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get Subscriptions", "Get all subscriptions or filter by organizationId.")]
    public async Task<IActionResult> GetSubscriptions(
        [FromQuery] int? organizationId,
        CancellationToken cancellationToken)
    {
        if (organizationId.HasValue)
        {
            var result = await subscriptionQueryService.Handle(
                new GetSubscriptionByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(result.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var subscriptions = await subscriptionQueryService.Handle(
            new GetAllSubscriptionsQuery(),
            cancellationToken);

        return Ok(subscriptions.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }
    
    [AllowAnonymous]
    [HttpPost]
    [SwaggerOperation("Create Subscription", "Create a new subscription.")]
    public async Task<IActionResult> CreateSubscription(
        [FromBody] CreateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);

        return SubscriptionActionResultAssembler.ToActionResultFromCreateSubscriptionResult(
            this,
            result,
            problemDetailsFactory,
            subscription => CreatedAtAction(
                nameof(GetSubscriptions),
                new { organizationId = subscription.OrganizationId },
                SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription)
            )
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update Subscription", "Update the plan of an existing subscription.")]
    public async Task<IActionResult> UpdateSubscription(
        int id,
        [FromBody] UpdateSubscriptionResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);

        return SubscriptionActionResultAssembler.ToActionResultFromUpdateSubscriptionResult(
            this,
            result,
            problemDetailsFactory,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription))
        );
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Cancel Subscription", "Cancel an existing subscription.")]
    public async Task<IActionResult> CancelSubscription(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new CancelSubscriptionCommand(id);
        var result = await subscriptionCommandService.Handle(command, cancellationToken);

        return SubscriptionActionResultAssembler.ToActionResultFromUpdateSubscriptionResult(
            this,
            result,
            problemDetailsFactory,
            subscription => Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription))
        );
    }

    [HttpGet("{id:int}/access-status")]
    [SwaggerOperation("Get Subscription Access Status", "Check if a subscription is active and grants platform access.")]
    public async Task<IActionResult> GetSubscriptionAccessStatus(
        int id,
        CancellationToken cancellationToken)
    {
        var subscription = await subscriptionQueryService.Handle(
            new GetSubscriptionByIdQuery(id), cancellationToken);

        if (subscription is null)
            return NotFound(new { Message = "Subscription not found." });

        return Ok(new
        {
            SubscriptionId = subscription.Id,
            OrganizationId = subscription.OrganizationId,
            Status = subscription.Status.ToString(),
            HasAccess = subscription.Status.ToString() == "Active"
        });
    }

    [HttpGet("{id:int}/summary")]
    [SwaggerOperation("Get Subscription Summary", "Get a summary of the subscription including plan and status details.")]
    public async Task<IActionResult> GetSubscriptionSummary(
        int id,
        CancellationToken cancellationToken)
    {
        var subscription = await subscriptionQueryService.Handle(
            new GetSubscriptionByIdQuery(id), cancellationToken);

        if (subscription is null)
            return NotFound(new { Message = "Subscription not found." });

        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(subscription));
    }
}