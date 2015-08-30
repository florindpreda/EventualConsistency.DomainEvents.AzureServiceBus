using EvCoSample.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain.Events
{
	public class PlaceholderEvent : DomainEvent
	{
		public string PlaceHolderProperty { get; set; }
	}
}