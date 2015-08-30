using EvCoSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain
{
	public interface IStoredEventRepository
	{
		IEnumerable<StoredEvent> GetNewEvents();

		void Add(StoredEvent storedEvent);
		void Update(StoredEvent storedEvent);
	}
}
