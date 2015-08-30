using EvCoSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain.Models
{
	public class StoredEvent : Entity
	{
		public string TypeName { get; private set; }
		public DateTime OccurredOn { get; private set; }
		public string SerializedBody { get; private set; }
		public bool IsForwarded { get; private set; }

		protected StoredEvent() {}

		public StoredEvent(string typeName, DateTime occurredOn, string serializedBody)
		{
			TypeName = typeName;
			OccurredOn = occurredOn;
			SerializedBody = serializedBody;
		}

		public void MarkAsForwarded()
		{
			IsForwarded = true;
		}
	}
}