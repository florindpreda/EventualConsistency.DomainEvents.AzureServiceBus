using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Core;
using EvCoSample.Core.Events;

namespace EvCoSample.Domain.Events
{
	public class ProductDeleted : DomainEvent
	{
		public Guid ProductId { get; protected set; }		

		public ProductDeleted(Guid productId)
		{
			this.ProductId = productId;			
		}
	}
}