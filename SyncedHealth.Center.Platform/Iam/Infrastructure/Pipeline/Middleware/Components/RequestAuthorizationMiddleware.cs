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
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        var cancellationToken = context.RequestAborted;

        Console.WriteLine("Entering InvokeAsync");

        var allowAnonymous = context.GetEndpoint()?.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        Console.WriteLine($"Allow Anonymous is {allowAnonymous}");

        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }

        Console.WriteLine("Entering authorization");

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

        Console.WriteLine("Continuing with Middleware Pipeline");

        await next(context);
    }
}