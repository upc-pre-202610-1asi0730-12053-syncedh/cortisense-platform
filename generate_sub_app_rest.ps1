$basePath = "C:\Users\marsr\cortisense-platform\SyncedHealth.Center.Platform"

# --- Subscription Application & Interfaces ---

# Resources
Set-Content -Path "$basePath\Subscription\Interfaces\REST\Resources\CreateSubscriptionPlanResource.cs" -Value @"
namespace SyncedHealth.Center.Platform.Subscription.Interfaces.REST.Resources;

public record CreateSubscriptionPlanResource(string Name, decimal Price, int MaxUsers);
"@

Set-Content -Path "$basePath\Subscription\Interfaces\REST\Resources\SubscriptionPlanResource.cs" -Value @"
namespace SyncedHealth.Center.Platform.Subscription.Interfaces.REST.Resources;

public record SubscriptionPlanResource(int Id, string Name, decimal Price, int MaxUsers);
"@

# Commands
Set-Content -Path "$basePath\Subscription\Application\Internal\CommandServices\CreateSubscriptionPlanCommand.cs" -Value @"
namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;

public record CreateSubscriptionPlanCommand(string Name, decimal Price, int MaxUsers);
"@

# Services Interfaces
Set-Content -Path "$basePath\Subscription\Domain\Services\ISubscriptionCommandService.cs" -Value @"
using SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<int> Handle(CreateSubscriptionPlanCommand command);
}
"@

# Services Implementation
Set-Content -Path "$basePath\Subscription\Application\Internal\CommandServices\SubscriptionCommandService.cs" -Value @"
using SyncedHealth.Center.Platform.Shared.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Domain.Repositories;
using SyncedHealth.Center.Platform.Subscription.Domain.Services;

namespace SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;

public class SubscriptionCommandService : ISubscriptionCommandService
{
    private readonly ISubscriptionPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionCommandService(ISubscriptionPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateSubscriptionPlanCommand command)
    {
        var plan = new SubscriptionPlan
        {
            Name = command.Name,
            Price = command.Price,
            MaxUsers = command.MaxUsers
        };

        await _planRepository.AddAsync(plan);
        await _unitOfWork.CompleteAsync();

        return plan.Id;
    }
}
"@

# Controllers
Set-Content -Path "$basePath\Subscription\Interfaces\REST\SubscriptionPlansController.cs" -Value @"
using Microsoft.AspNetCore.Mvc;
using SyncedHealth.Center.Platform.Subscription.Application.Internal.CommandServices;
using SyncedHealth.Center.Platform.Subscription.Domain.Services;
using SyncedHealth.Center.Platform.Subscription.Interfaces.REST.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.REST;

[ApiController]
[Route(`"api/v1/subscription-plans`")]
public class SubscriptionPlansController : ControllerBase
{
    private readonly ISubscriptionCommandService _subscriptionCommandService;

    public SubscriptionPlansController(ISubscriptionCommandService subscriptionCommandService)
    {
        _subscriptionCommandService = subscriptionCommandService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreateSubscriptionPlanResource resource)
    {
        var command = new CreateSubscriptionPlanCommand(resource.Name, resource.Price, resource.MaxUsers);
        var planId = await _subscriptionCommandService.Handle(command);

        return CreatedAtAction(nameof(CreatePlan), new { id = planId }, new { Id = planId, resource.Name, resource.Price });
    }
}
"@

Write-Host "Subscription App and REST layer generated."
