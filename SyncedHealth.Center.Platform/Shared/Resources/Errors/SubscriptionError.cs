namespace SyncedHealth.Center.Platform.Shared.Resources.Errors;

public enum SubscriptionError
{
    PlanNotFound,
    SubscriptionNotFound,
    CheckoutSessionNotFound,
    InvalidSubscriptionState,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
