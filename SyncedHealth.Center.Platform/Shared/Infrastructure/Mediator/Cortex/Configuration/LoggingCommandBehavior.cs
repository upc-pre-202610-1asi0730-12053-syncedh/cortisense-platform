using Cortex.Mediator.Commands;

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
/// Represents the logging command behavior in the CortiSense Platform.
/// </summary>
public class LoggingCommandBehavior<TCommand>
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    public async Task Handle(
        TCommand command,
        CommandHandlerDelegate next,
        CancellationToken ct)
    {
        // Log before/after
        await next();
    }
}
