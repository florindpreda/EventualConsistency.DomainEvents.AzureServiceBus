using EvCoSample.Core;
using EvCoSample.Core.Events;
using EvCoSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvCoSample.Application
{
	public interface IMessagingService
	{
		void ProcessNextEvent<TEvent>(Action<TEvent> handle, string queueName) where TEvent : DomainEvent;
		void Send(StoredEvent storedEvent, string queueName);
	}
}
