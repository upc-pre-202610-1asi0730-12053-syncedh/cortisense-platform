using SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;
using SyncedHealth.Center.Platform.Subscription.Domain.Model.ValueObjects;

namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Aggregates;

public partial class CheckoutSession
{
    public CheckoutSession()
    {
        PlanCode = string.Empty;
    }

    public CheckoutSession(CreateCheckoutSessionCommand command)
    {
        OrganizationId = command.OrganizationId;
        AdministratorId = command.AdministratorId;
        SubscriptionId = command.SubscriptionId;
        PlanId = command.PlanId;
        PlanCode = command.PlanCode;
        Status = ECheckoutSessionStatus.Pending;
    }

    public int Id { get; private set; }
    public int OrganizationId { get; private set; }
    public int AdministratorId { get; private set; }
    public int SubscriptionId { get; private set; }
    public int PlanId { get; private set; }
    public string PlanCode { get; private set; }
    public ECheckoutSessionStatus Status { get; private set; }

    public void MarkAsCompleted()
    {
        Status = ECheckoutSessionStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = ECheckoutSessionStatus.Failed;
    }
}
