using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

/**
 * RequestAuthorizationMiddlewareExtensions
 * This class includes a method extension to register RequestAuthorizationMiddleware in the ASP.NET Core pipeline.
 */
/// <summary>
/// Represents the request authorization middleware extensions in the CortiSense Platform.
/// </summary>
public static class RequestAuthorizationMiddlewareExtensions
{
    /**
     * UseRequestAuthorization extension method is used to register RequestAuthorizationMiddleware in the ASP.NET Core pipeline.
     */
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}