using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using SyncedHealth.Center.Platform.Iam.Application.CommandServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.Iam.Interfaces.Rest.Transform;
using SyncedHealth.Center.Platform.Iam.Resources;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/organizations")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Organization endpoints")]
/// <summary>
/// Controller for organizations REST API endpoints.
/// </summary>
public class OrganizationsController(
    IOrganizationCommandService organizationCommandService,
    IOrganizationQueryService organizationQueryService,
    IStringLocalizer<IamMessages> iamLocalizer)
    : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Get Organizations", "Get all organizations or filter by email.")]
    public async Task<IActionResult> GetOrganizations(
        [FromQuery] string? email,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var organization = await organizationQueryService.Handle(
                new GetOrganizationByEmailQuery(email),
                cancellationToken);

            if (organization is null)
                return Ok(Array.Empty<OrganizationResource>());

            return Ok(new[]
            {
                OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization)
            });
        }

        var organizations = await organizationQueryService.Handle(
            new GetAllOrganizationsQuery(),
            cancellationToken);

        return Ok(organizations.Select(OrganizationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation("Get Organization by Id", "Get an organization by its identifier.")]
    public async Task<IActionResult> GetOrganizationById(
        int id,
        CancellationToken cancellationToken)
    {
        var organization = await organizationQueryService.Handle(
            new GetOrganizationByIdQuery(id),
            cancellationToken);

        if (organization is null)
            return NotFound(new { message = iamLocalizer["OrganizationNotFound"].Value });

        return Ok(OrganizationResourceFromEntityAssembler.ToResourceFromEntity(organization));
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create Organization", "Create a new organization.")]
    public async Task<IActionResult> CreateOrganization(
        [FromBody] CreateOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateOrganizationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return CreatedAtAction(
            nameof(GetOrganizationById),
            new { id = result.Value!.Id },
            OrganizationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!)
        );
    }

    [HttpPatch("{id:int}")]
    [AllowAnonymous]
    [SwaggerOperation("Update Organization", "Update an organization.")]
    public async Task<IActionResult> UpdateOrganization(
        int id,
        [FromBody] UpdateOrganizationResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateOrganizationCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await organizationCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return Ok(OrganizationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}