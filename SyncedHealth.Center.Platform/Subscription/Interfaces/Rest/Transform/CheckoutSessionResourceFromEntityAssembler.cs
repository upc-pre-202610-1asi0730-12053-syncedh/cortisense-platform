using SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;
using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class CheckoutSessionResourceFromEntityAssembler
{
    public static CheckoutSessionResource ToResourceFromEntity(CheckoutSession entity)
    {
        return new CheckoutSessionResource
        {
            Id = entity.Id,
            OrganizationId = entity.OrganizationId,
            AdministratorId = entity.AdministratorId,
            SubscriptionId = entity.SubscriptionId,
            PlanId = entity.PlanId,
            PlanCode = entity.PlanCode,
            Status = entity.Status.ToString()
        };
    }
}
