using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public interface IEventSerializer
	{
		string Serialize<TEvent>(TEvent domainEvent) where TEvent : DomainEvent;
		TEvent Deserialize<TEvent>(string serializedEvent) where TEvent : DomainEvent;
	}
}
