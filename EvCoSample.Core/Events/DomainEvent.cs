using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core.Events
{
	public abstract class DomainEvent
	{
		public DateTime OcurrendOn { get; protected set; }

		public DomainEvent()
		{
			this.OcurrendOn = DateTime.UtcNow;
		}
	}
}
