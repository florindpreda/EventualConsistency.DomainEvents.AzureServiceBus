using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public class DomainEventPublisher
	{
		private readonly IDictionary<Type, IList<IDomainEventSubscriber>> _subscribers = new Dictionary<Type, IList<IDomainEventSubscriber>>();

		public void Publish<T>(T domainEvent) where T : DomainEvent
		{			
			var eventSubscribers = _subscribers.SelectMany(s => s.Value)
												.Where(sb => sb.SubscribedToEventType() == domainEvent.GetType()
															|| sb.SubscribedToEventType() == typeof(DomainEvent)
														);

			foreach(var eventSubscriber in eventSubscribers)
			{
				eventSubscriber.Handle(domainEvent);
			}
		}		

		public void Subscribe<TEvent>(Action<DomainEvent> handle) where TEvent : DomainEvent
		{
			var subscriber = new DomainEventSubscriber(handle, typeof(TEvent));
			Subscribe(subscriber);
		}

		public void Subscribe(IDomainEventSubscriber domainEventSubscriber)
		{
			var eventType = domainEventSubscriber.SubscribedToEventType();			
			if (_subscribers.ContainsKey(eventType))
			{
				_subscribers[eventType].Add(domainEventSubscriber);
			}
			else
			{
				_subscribers[eventType] = new List<IDomainEventSubscriber>();
				_subscribers[eventType].Add(domainEventSubscriber);
			}
		}		
	}
}