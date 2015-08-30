using EvCoSample.Core;
using EvCoSample.Core.Events;
using EvCoSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain.EventsSubscribers
{
	public class EventStoringSubscriber : IEventStoringSubscriber
	{		
		private readonly IStoredEventRepository _storedEventRepository;
		private readonly IEventSerializer _eventSerializer;

		public EventStoringSubscriber(IStoredEventRepository storedEventRepository, IEventSerializer eventSerializer)
		{			
			_storedEventRepository = storedEventRepository;
			_eventSerializer = eventSerializer;
		}

		public void Handle(DomainEvent domainEvent)
		{
			var serializedBody = _eventSerializer.Serialize(domainEvent);
			var storedEvent = new StoredEvent(domainEvent.GetType().ToString(), domainEvent.OcurrendOn, serializedBody);
			_storedEventRepository.Add(storedEvent);
		}

		public Type SubscribedToEventType()
		{
			return typeof(DomainEvent);
		}
	}
}