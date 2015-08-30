using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using EvCoSample.Domain;

namespace EvCoSample.Infrastructure.Migrations
{    

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
			Seeder.Seed(context);			
        }
    }
}
