using EvCoSample.Core;
using EvCoSample.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain.EventsSubscribers
{
	public interface IEventStoringSubscriber : IDomainEventSubscriber
	{
	}
}
