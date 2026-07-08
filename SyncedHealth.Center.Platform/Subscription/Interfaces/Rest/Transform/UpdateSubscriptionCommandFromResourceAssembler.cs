using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

/// <summary>
/// Represents the update subscription command from resource assembler in the CortiSense Platform.
/// </summary>
public static class UpdateSubscriptionCommandFromResourceAssembler
{
    public static UpdateSubscriptionCommand ToCommandFromResource(int subscriptionId, UpdateSubscriptionResource resource)
    {
        return new UpdateSubscriptionCommand(subscriptionId, resource.PlanId);
    }
}
