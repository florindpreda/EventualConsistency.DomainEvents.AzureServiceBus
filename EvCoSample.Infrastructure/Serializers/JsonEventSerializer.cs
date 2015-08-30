using EvCoSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EvCoSample.Core.Events;

namespace EvCoSample.Infrastructure.Serializers
{
	public class JsonEventSerializer : IEventSerializer
	{
		public string Serialize<TEvent>(TEvent domainEvent) where TEvent : DomainEvent
		{		
			var serializedEvent = JsonConvert.SerializeObject(domainEvent);

			return serializedEvent;
		}

		public TEvent Deserialize<TEvent>(string serializedEvent) where TEvent : DomainEvent
		{
			var domainEvent = JsonConvert.DeserializeObject<TEvent>(serializedEvent);

			return domainEvent;
		}
	}
}