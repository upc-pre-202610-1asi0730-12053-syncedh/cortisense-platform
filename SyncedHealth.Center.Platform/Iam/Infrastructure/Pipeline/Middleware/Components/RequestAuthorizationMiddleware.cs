using SyncedHealth.Center.Platform.Iam.Application.Internal.OutboundServices;
using SyncedHealth.Center.Platform.Iam.Application.QueryServices;
using SyncedHealth.Center.Platform.Iam.Domain.Model.Queries;
using SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace SyncedHealth.Center.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    // Public paths that do not require authentication
    private static readonly HashSet<string> PublicPaths = new(StringComparer.OrdinalIgnoreCase)
    {
        "/api/v1/authentication/sign-in",
        "/api/v1/authentication/sign-up",
        "/api/v1/invitations/validate",
        "/api/v1/invitations/accept",
        "/api/v1/plans",
        "/api/v1/specialties",
        "/api/v1/workAreas",
        "/api/v1/organizations",
        "/api/v1/subscriptions",
        "/api/v1/checkoutSessions",
    };

    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var cancellationToken = context.RequestAborted;

        // Check [AllowAnonymous] attribute on endpoint (works if routing already ran)
        var allowAnonymous = context.GetEndpoint()?.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        // Also check our explicit public-paths whitelist
        if (!allowAnonymous)
        {
            var path = context.Request.Path.Value ?? string.Empty;
            allowAnonymous = PublicPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase));
        }

        if (allowAnonymous)
        {
            await next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Unauthorized",
                status = 401,
                detail = "Null or invalid token"
            }, cancellationToken);

            return;
        }

        var userId = await tokenService.ValidateToken(token);

        if (userId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Unauthorized",
                status = 401,
                detail = "Invalid token"
            }, cancellationToken);

            return;
        }

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);

        context.Items["User"] = user;

        await next(context);
    }
}