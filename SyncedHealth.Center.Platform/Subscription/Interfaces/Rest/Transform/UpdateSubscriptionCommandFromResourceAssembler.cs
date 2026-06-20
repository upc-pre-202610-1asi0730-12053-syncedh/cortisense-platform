using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(int subscriptionId, UpdateSubscriptionResource resource)
    {
        return new UpdateSubscriptionCommand(subscriptionId, resource.PlanId);
    }
}
