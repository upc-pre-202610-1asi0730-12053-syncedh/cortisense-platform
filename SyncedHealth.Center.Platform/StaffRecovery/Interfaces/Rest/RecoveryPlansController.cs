using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.StaffRecovery.Application.CommandServices;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Resources;
using SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest.Transform;

namespace SyncedHealth.Center.Platform.StaffRecovery.Interfaces.Rest;

[ApiController]
[Route("api/v1/recovery-plans")]
[Produces(MediaTypeNames.Application.Json)]
public class RecoveryPlansController : ControllerBase
{
    private readonly IRecoveryPlanCommandService _recoveryPlanCommandService;

    public RecoveryPlansController(IRecoveryPlanCommandService recoveryPlanCommandService)
    {
        _recoveryPlanCommandService = recoveryPlanCommandService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecoveryPlan([FromBody] CreateRecoveryPlanResource resource)
    {
        var command = CreateRecoveryPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var recoveryPlan = await _recoveryPlanCommandService.Handle(command);
        
        if (recoveryPlan is null) 
            
            return BadRequest();
        
        var resourceResponse = RecoveryPlanResourceFromEntityAssembler.ToResourceFromEntity(recoveryPlan);
        
        return StatusCode(201, resourceResponse);
    }
}