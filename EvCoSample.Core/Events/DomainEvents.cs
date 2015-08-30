using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public class DomainEvents
	{
		private static DomainEventPublisher _publisher;

		public static DomainEventPublisher Publisher 
		{ 
			get 
			{ 
				if (_publisher == null)
				{
					throw new Exception("Publisher is not initialized");
				}
				return _publisher; 
			} 
		}

		public DomainEvents(DomainEventPublisher publisher)
		{
			_publisher = publisher;
		}
	}
}
