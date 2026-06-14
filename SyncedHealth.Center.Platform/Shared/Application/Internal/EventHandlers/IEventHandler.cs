using SyncedHealth.Center.Platform.Shared.Domain.Model.Events;

using Cortex.Mediator.Notifications;

namespace SyncedHealth.Center.Platform.Shared.Application.Internal.EventHandlers;


public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
