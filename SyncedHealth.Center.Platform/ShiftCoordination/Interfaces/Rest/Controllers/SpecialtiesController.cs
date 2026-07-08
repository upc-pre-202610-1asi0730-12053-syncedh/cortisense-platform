using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.CommandServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Application.QueryServices;
using SyncedHealth.Center.Platform.ShiftCoordination.Domain.Model.Queries;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.ShiftCoordination.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/specialties")]
/// <summary>
/// Controller for specialties REST API endpoints.
/// </summary>
public class SpecialtiesController(
    ISpecialtyCommandService specialtyCommandService,
    ISpecialtyQueryService specialtyQueryService)
    : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllSpecialties(CancellationToken cancellationToken)
    {
        var specialties = await specialtyQueryService.Handle(
            new GetAllSpecialtiesQuery(),
            cancellationToken);

        var resources = specialties
            .Select(SpecialtyResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSpecialtyById(
        int id,
        CancellationToken cancellationToken)
    {
        var specialty = await specialtyQueryService.Handle(
            new GetSpecialtyByIdQuery(id),
            cancellationToken);

        if (specialty is null)
            return NotFound();

        var resource = SpecialtyResourceFromEntityAssembler.ToResourceFromEntity(specialty);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecialty(
        [FromBody] CreateSpecialtyResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateSpecialtyCommandFromResourceAssembler.ToCommandFromResource(resource);

        var result = await specialtyCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess)
            return ShiftCoordinationActionResultAssembler.ToActionResult(result);

        var specialtyResource = SpecialtyResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);

        return CreatedAtAction(
            nameof(GetSpecialtyById),
            new { id = specialtyResource.Id },
            specialtyResource);
    }
}