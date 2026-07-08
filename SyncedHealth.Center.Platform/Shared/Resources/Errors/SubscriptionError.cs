namespace SyncedHealth.Center.Platform.Shared.Resources.Errors;

/// <summary>
/// Represents the subscription error in the CortiSense Platform.
/// </summary>
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
