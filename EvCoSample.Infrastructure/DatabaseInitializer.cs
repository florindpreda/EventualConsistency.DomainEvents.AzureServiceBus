using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Domain;

namespace EvCoSample.Infrastructure
{
	public class DatabaseInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
			Seeder.Seed(context);

            base.Seed(context);
        }
    }
}
