using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Core;

namespace EvCoSample.Infrastructure
{
	public class EntityFrameworkConfiguration : IDatabaseConfiguration
	{
		public void Initialise()
		{
			Database.SetInitializer<DatabaseContext>(new DatabaseInitializer());
			var ctx = new DatabaseContext();
			ctx.Database.Initialize(false);
		}
	}
}
