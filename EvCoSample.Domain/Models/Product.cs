using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Core;
using EvCoSample.Core.Events;
using EvCoSample.Domain.Events;

namespace EvCoSample.Domain
{
    public class Product : Entity
    {		
		public string Name { get; private set; }

		protected Product() {}

		public Product(string name)
		{		
			this.Name = name;
		}

		public override void Delete()
		{
			base.Delete();
			DomainEvents.Publisher.Publish<ProductDeleted>(new ProductDeleted(this.Id));			
		}
    }
}
