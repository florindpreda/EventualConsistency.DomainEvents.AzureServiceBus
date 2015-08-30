using EvCoSample.Core;
using EvCoSample.Domain;
using EvCoSample.Domain.Events;
using EvCoSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Application
{
	public class EventForwarderService : IEventForwarderService
	{		
		private readonly IDictionary<string, string> _eventTypeQueueMapping = new Dictionary<string, string>()
		{
			{ typeof(ProductDeleted).ToString(), "ProductDeletedQueue" },
			{ typeof(PlaceholderEvent).ToString(), "PlaceholderQueue" }
		};

		private readonly IUnitOfWork _unitOfWork;
		private readonly IStoredEventRepository _storedEventRepository;
		private readonly IMessagingService _messagingService;

		public EventForwarderService(IUnitOfWork unitOfWork, IStoredEventRepository storedEventRepository, IMessagingService messagingService)
		{
			_unitOfWork = unitOfWork;
			_storedEventRepository = storedEventRepository;
			_messagingService = messagingService;
		}

		public void ForwardEvents()
		{
			using(_unitOfWork)
			{
				var newEvents = _storedEventRepository.GetNewEvents().ToList();
				
				foreach(StoredEvent storedEvent in newEvents)
				{
					var queueName = this.GetAssociatedQueueName(storedEvent.TypeName);
					_messagingService.Send(storedEvent, queueName);

					storedEvent.MarkAsForwarded();
					_storedEventRepository.Update(storedEvent);

					_unitOfWork.Commit();
				}				
			}
		}

		private string GetAssociatedQueueName(string eventType)
		{
			var queueName = string.Empty;

			try
			{
				queueName = _eventTypeQueueMapping[eventType];
			}
			catch(KeyNotFoundException ex)
			{
				throw new ArgumentOutOfRangeException(string.Format("No mapping defined for event: {0}", eventType), ex);
			}

			return queueName;
		}
	}
}
