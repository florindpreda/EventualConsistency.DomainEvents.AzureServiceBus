using EvCoSample.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Core
{
	public abstract class Entity
	{
		public Guid Id { get; private set; }
		public bool IsDeleted { get; private set; }

		public Entity()
		{
			this.Id = Guid.NewGuid();
			this.IsDeleted = false;
		}

		public virtual void Delete()
		{
			this.IsDeleted = true;			
		}
	}
}
