namespace SyncedHealth.Center.Platform.Subscription.Domain.Model.Commands;

public record CreateCheckoutSessionCommand(int OrganizationId, int AdministratorId, int SubscriptionId, int PlanId, string PlanCode);
