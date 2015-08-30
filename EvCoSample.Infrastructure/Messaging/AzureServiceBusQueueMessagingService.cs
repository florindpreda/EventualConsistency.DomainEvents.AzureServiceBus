using EvCoSample.Application;
using EvCoSample.Core;
using EvCoSample.Core.Events;
using EvCoSample.Domain.Events;
using EvCoSample.Domain.Models;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Infrastructure.Messaging
{
	public class AzureServiceBusQueueMessagingService : IMessagingService
	{		
		private readonly NamespaceManager _namespaceManager;
		private readonly string _connectionString;

		private readonly IEventSerializer _eventSerializer;

		public AzureServiceBusQueueMessagingService(IEventSerializer eventSerializer)
		{
			_connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
			_namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionString);			
			_eventSerializer = eventSerializer;
		}

		private void InitQueue(string queueName)
		{
			if(string.IsNullOrWhiteSpace(queueName))
			{
				throw new ArgumentNullException("Queue name is empty.");
			}

			//configure queue settings
			var queueDescription = new QueueDescription(queueName);
			queueDescription.RequiresDuplicateDetection = true;
			queueDescription.DuplicateDetectionHistoryTimeWindow = TimeSpan.FromDays(7);
			queueDescription.LockDuration = TimeSpan.FromMinutes(5);
			queueDescription.EnableDeadLetteringOnMessageExpiration = true;

			//create queue if not exists
			if (!_namespaceManager.QueueExists(queueName))
			{
				_namespaceManager.CreateQueue(queueDescription);
			}
		}

		public void Send(StoredEvent storedEvent, string queueName)
		{
			this.InitQueue(queueName);

			var client = QueueClient.CreateFromConnectionString(_connectionString, queueName);			
			var brokeredMessage = this.CreateBrokeredMessage(storedEvent);

			client.Send(brokeredMessage);

			client.Close();
		}

		private BrokeredMessage CreateBrokeredMessage(StoredEvent storedEvent)
		{
			var brokeredMessage = new BrokeredMessage(storedEvent.SerializedBody);
			brokeredMessage.MessageId = storedEvent.Id.ToString();

			return brokeredMessage;
		}

		public void ProcessNextEvent<TEvent>(Action<TEvent> handle, string queueName) where TEvent : DomainEvent
		{
			this.InitQueue(queueName);

			var client = QueueClient.CreateFromConnectionString(_connectionString, queueName);

			var brokeredMessage = client.Receive(TimeSpan.FromSeconds(5));

			if (brokeredMessage != null)
			{
				Process<TEvent>(handle, brokeredMessage);
			}
		}

		private void Process<TEvent>(Action<TEvent> handle, BrokeredMessage brokeredMessage) where TEvent : DomainEvent
		{
			var jsonEvent = brokeredMessage.GetBody<string>();
			var productDeletedEvent = _eventSerializer.Deserialize<TEvent>(jsonEvent);

			handle(productDeletedEvent);

			try
			{				
				brokeredMessage.Complete();
			}
			catch (Exception ex)
			{
				//do something else, e.g log
				brokeredMessage.DeadLetter();//move to dead letter queue to inspect later	
			}
		}
	}
}
