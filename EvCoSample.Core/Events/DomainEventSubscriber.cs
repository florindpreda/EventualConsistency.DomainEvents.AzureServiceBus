using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public class DomainEventSubscriber : IDomainEventSubscriber
	{				
		private readonly Action<DomainEvent> _handle;
		private readonly Type _subType;		

		public DomainEventSubscriber(Action<DomainEvent> handle, Type subType)
		{
			_handle = handle;
			_subType = subType;
		}

		public void Handle(DomainEvent domainEvent)
		{
			_handle(domainEvent);
		}

		public Type SubscribedToEventType()
		{
			return _subType;
		}
	}
}