using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public interface IDomainEventSubscriber
	{
		void Handle(DomainEvent domainEvent);
		Type SubscribedToEventType();
	}
}