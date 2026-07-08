namespace SyncedHealth.Center.Platform.Shared.Domain.Repositories;

/// <summary>
/// Represents the unit of work in the CortiSense Platform.
/// </summary>
public interface IUnitOfWork
{
    Task CompleteAsync(CancellationToken cancellationToken = default);
}
