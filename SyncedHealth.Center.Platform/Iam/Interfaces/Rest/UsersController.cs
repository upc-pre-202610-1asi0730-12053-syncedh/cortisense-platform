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
using SyncedHealth.Center.Platform.Shared.Interfaces.Rest.ProblemDetails;
using SyncedHealth.Center.Platform.Shared.Resources.Errors;

namespace SyncedHealth.Center.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get User by Id", "Get a user by its identifier.")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(id), cancellationToken);

        return IamActionResultAssembler.ToActionResultFromGetUserByIdResult(
            this,
            user,
            _errorLocalizer,
            _problemDetailsFactory,
            foundUser => Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(foundUser))
        );
    }

    [HttpGet]
    [SwaggerOperation("Get Users", "Get all users or filter by organizationId/email.")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int? organizationId,
        [FromQuery] string? email,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var user = await userQueryService.Handle(
                new GetUserByEmailQuery(email),
                cancellationToken);

            if (user is null)
                return Ok(Array.Empty<UserResource>());

            return Ok(new[] { UserResourceFromEntityAssembler.ToResourceFromEntity(user) });
        }

        if (organizationId.HasValue)
        {
            var usersByOrganization = await userQueryService.Handle(
                new GetUsersByOrganizationIdQuery(organizationId.Value),
                cancellationToken);

            return Ok(usersByOrganization.Select(UserResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var users = await userQueryService.Handle(new GetAllUsersQuery(), cancellationToken);

        return Ok(users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [AllowAnonymous]
    [SwaggerOperation("Create User", "Create a user. Used by invitation registration flow.")]
    public async Task<IActionResult> CreateUser(
        [FromBody] SignUpResource resource,
        CancellationToken cancellationToken)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = result.Value!.Id },
            UserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!)
        );
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation("Update User", "Update user profile, role, status or password.")]
    public async Task<IActionResult> UpdateUser(
        int id,
        [FromBody] UpdateUserResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await userCommandService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(new { message = result.Message });

        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}