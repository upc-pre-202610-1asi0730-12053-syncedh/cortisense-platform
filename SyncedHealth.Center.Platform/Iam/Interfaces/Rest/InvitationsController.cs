using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;
using SyncedHealth.Center.Platform.Iam.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/invitations")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Invitation endpoints")]
public class InvitationsController(
    IInvitationCommandService invitationCommandService,
    IInvitationQueryService invitationQueryService,
    IInvitationEmailService invitationEmailService,
    IStringLocalizer<IamMessages> iamLocalizer)
    : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get Invitations", "Get invitations or filter by organizationId, token or email.")]
    public async Task<IActionResult> GetInvitations(
        [FromQuery] int? organizationId,
        [FromQuery] string? token,
        [FromQuery] string? email,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(token))
        {
            var invitation = await invitationQueryService.Handle(
                new GetInvitationByTokenQuery(token),
                cancellationToken);

            if (invitation is null)
                return Ok(Array.Empty<InvitationResource>());

            return Ok(new[]
            {
                InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation)
            });
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var invitationsByEmail = await invitationQueryService.Handle(
                new GetInvitationsByEmailQuery(email),
                cancellationToken);

            return Ok(invitationsByEmail.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (organizationId.HasValue)
        {
            var invitationsByOrganization = await invitationQueryService.Handle(
                new GetInvitationsByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(invitationsByOrganization.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var invitations = await invitationQueryService.Handle(
            new GetAllInvitationsQuery(),
            cancellationToken);

        return Ok(invitations.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation("Get Invitation by Id", "Get an invitation by its identifier.")]
    public async Task<IActionResult> GetInvitationById(
        int id,
        CancellationToken cancellationToken)
    {
        var invitation = await invitationQueryService.Handle(
            new GetInvitationByIdQuery(id),
            cancellationToken);

        if (invitation is null)
            return NotFound(new { message = iamLocalizer["InvitationNotFound"].Value });

        return Ok(InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation));
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create Invitation", "Create an invitation without sending email.")]
    public async Task<IActionResult> CreateInvitation(
        [FromBody] CreateInvitationResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateInvitationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await invitationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return CreatedAtAction(
            nameof(GetInvitationById),
            new { id = result.Value!.Id },
            InvitationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!)
        );
    }

    [HttpPost("send")]
    [AllowAnonymous]
    [SwaggerOperation("Send Invitation", "Create an invitation and send it using Resend.")]
    public async Task<IActionResult> SendInvitation(
        [FromBody] SendInvitationResource resource,
        CancellationToken cancellationToken)
    {
        var createCommand = SendInvitationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var createResult = await invitationCommandService.Handle(createCommand, cancellationToken);

        if (createResult.IsFailure)
            return BadRequest(new { message = createResult.Message });

        var invitation = createResult.Value!;

        var emailResult = await invitationEmailService.SendInvitationAsync(
            invitation.Email,
            invitation.Token,
            cancellationToken);

        var nextStatus = emailResult.EmailStatus == "SENT"
            ? "SENT"
            : "PENDING";

        var updateCommand = new UpdateInvitationCommand(
            invitation.Id,
            null,
            null,
            nextStatus,
            emailResult.EmailStatus,
            emailResult.ResendEmailId,
            emailResult.ErrorMessage,
            null
        );

        var updateResult = await invitationCommandService.Handle(updateCommand, cancellationToken);

        if (updateResult.IsFailure)
            return BadRequest(new { message = updateResult.Message });

        return CreatedAtAction(
            nameof(GetInvitationById),
            new { id = updateResult.Value!.Id },
            InvitationResourceFromEntityAssembler.ToResourceFromEntity(updateResult.Value!)
        );
    }

    [HttpPatch("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation("Update Invitation", "Update an invitation.")]
    public async Task<IActionResult> UpdateInvitation(
        int id,
        [FromBody] UpdateInvitationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateInvitationCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await invitationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return Ok(InvitationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpDelete("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation("Cancel Invitation", "Cancel an invitation.")]
    public async Task<IActionResult> DeleteInvitation(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteInvitationCommand(id);
        var result = await invitationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return Ok(InvitationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPost("accept")]
    [AllowAnonymous]
    [SwaggerOperation("Accept Invitation", "Accept an invitation and create the invited user.")]
    public async Task<IActionResult> AcceptInvitation(
        [FromBody] AcceptInvitationResource resource,
        CancellationToken cancellationToken)
    {
        var command = AcceptInvitationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await invitationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return Ok(InvitationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}