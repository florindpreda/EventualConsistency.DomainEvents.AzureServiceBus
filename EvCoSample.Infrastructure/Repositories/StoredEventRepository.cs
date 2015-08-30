using EvCoSample.Domain;
using EvCoSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Infrastructure.Repositories
{
	public class StoredEventRepository : IStoredEventRepository
	{
		private readonly IDatabaseContext _databaseContext;

		public StoredEventRepository(IDatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		public IEnumerable<StoredEvent> GetNewEvents()
		{
			return _databaseContext.StoredEvents.Where(e => e.IsForwarded == false)
												.Where(e => e.IsDeleted == false)
												.OrderBy(e => e.OccurredOn);
		}	

		public void Add(StoredEvent storedEvent)
		{
			_databaseContext.StoredEvents.Add(storedEvent);
		}

		public void Update(StoredEvent storedEvent)
		{
			_databaseContext.Entry(storedEvent).State = System.Data.Entity.EntityState.Modified;
		}			
	}
}
