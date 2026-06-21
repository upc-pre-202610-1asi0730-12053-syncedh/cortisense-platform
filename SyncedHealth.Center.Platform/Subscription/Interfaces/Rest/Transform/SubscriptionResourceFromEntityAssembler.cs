using SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Resources;

namespace SyncedHealth.Center.Platform.Subscription.Interfaces.Rest.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Domain.Model.Aggregates.Subscription entity)
    {
        return new SubscriptionResource
        {
            Id = entity.Id,
            OrganizationId = entity.OrganizationId,
            PlanId = entity.PlanId,
            Status = entity.Status.ToString().ToUpperInvariant(),
            StartedAt = entity.StartedAt,
            CancelledAt = entity.CancelledAt,
            StripeSubscriptionId = entity.StripeSubscriptionId,
            StripeCustomerId = entity.StripeCustomerId
        };
    }
}