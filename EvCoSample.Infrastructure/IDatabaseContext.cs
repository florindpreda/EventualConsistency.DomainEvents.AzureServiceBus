using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Domain;
using EvCoSample.Domain.Models;

namespace EvCoSample.Infrastructure
{
	public interface IDatabaseContext
	{
		IDbSet<Product> Products { get; set; }
		IDbSet<ProductReview> ProductReviews { get; set; }
		IDbSet<StoredEvent> StoredEvents { get; set; }

		int SaveChanges();
		DbEntityEntry Entry(object entity);
		void Dispose();
	}
}
