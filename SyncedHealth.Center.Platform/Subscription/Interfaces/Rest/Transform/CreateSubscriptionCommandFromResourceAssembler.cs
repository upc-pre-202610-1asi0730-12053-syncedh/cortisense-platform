using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
    {
        return new CreateSubscriptionCommand(resource.OrganizationId, resource.PlanId);
    }
}
