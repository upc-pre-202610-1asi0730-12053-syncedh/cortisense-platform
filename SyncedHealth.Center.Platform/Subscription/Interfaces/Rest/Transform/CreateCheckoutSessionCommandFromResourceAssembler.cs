using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class CreateCheckoutSessionCommandFromResourceAssembler
{
    public static CreateCheckoutSessionCommand ToCommandFromResource(CreateCheckoutSessionResource resource)
    {
        return new CreateCheckoutSessionCommand(
            resource.OrganizationId,
            resource.AdministratorId,
            resource.SubscriptionId,
            resource.PlanId,
            resource.PlanCode
        );
    }
}
