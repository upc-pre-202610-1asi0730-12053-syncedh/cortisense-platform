using SyncedHealth.Center.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
/// Represents the middleware extensions in the CortiSense Platform.
/// </summary>
public static class MiddlewareExtensions
{
    /**
     * <summary>
     *     Use the global exception handler middleware
     * </summary>
     * <param name="builder">The application builder</param>
     * <returns>The application builder</returns>
     */
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
