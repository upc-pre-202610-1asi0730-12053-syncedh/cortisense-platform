using Cortex.Mediator.Notifications;
using SyncedHealth.Center.Platform.Shared.Domain.Model.Events;


namespace SyncedHealth.Center.Platform.Shared.Application.Internal.EventHandlers;


/// <summary>
/// Represents the event handler in the CortiSense Platform.
/// </summary>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
