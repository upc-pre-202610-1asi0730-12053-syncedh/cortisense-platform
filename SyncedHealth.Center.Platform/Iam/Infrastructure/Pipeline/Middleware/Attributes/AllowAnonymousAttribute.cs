namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

/**
 * This attribute is used to decorate controllers and actions that do not require authorization.
 * It skips authorization if the action is decorated with [AllowAnonymous] attribute.
 */
[AttributeUsage(AttributeTargets.Method)]
/// <summary>
/// Represents the allow anonymous attribute in the CortiSense Platform.
/// </summary>
public class AllowAnonymousAttribute : Attribute
{
}