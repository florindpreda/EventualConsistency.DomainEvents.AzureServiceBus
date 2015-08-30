using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EvCoSample.Application;
using EvCoSample.Core;
using EvCoSample.Domain;
using EvCoSample.Infrastructure;
using EvCoSample.Core.Events;
using EvCoSample.Domain.EventsSubscribers;
using EvCoSample.Infrastructure.Serializers;
using EvCoSample.Infrastructure.Repositories;
using EvCoSample.Infrastructure.Messaging;

namespace EvCoSample.DependencyResolver
{
    public class UnityConfig
    {
		private static UnityContainer _container;

		public static void RegisterComponents(UnityContainer container)
		{			
			_container = container;

			if (HttpContext.Current != null)
			{
				container.RegisterType<IDatabaseContext, DatabaseContext>(new PerHttpRequestLifetimeManager());
				container.RegisterType<DomainEventPublisher, DomainEventPublisher>(new PerHttpRequestLifetimeManager());
			}
			else
			{				
				container.RegisterType<IDatabaseContext, DatabaseContext>(new ContainerControlledLifetimeManager());
				container.RegisterType<DomainEventPublisher, DomainEventPublisher>(new ContainerControlledLifetimeManager());
			}

			container.RegisterType<IUnitOfWork, EvCoSample.Infrastructure.UnitOfWork>();
			container.RegisterType<IProductRepository, ProductRepository>();
			container.RegisterType<IProductReviewRepository, ProductReviewRepository>();
			container.RegisterType<IProductService, ProductService>();
			container.RegisterType<IDatabaseConfiguration, EntityFrameworkConfiguration>();

			container.RegisterType<IEventStoringSubscriber, EventStoringSubscriber>();
			container.RegisterType<IEventSerializer, JsonEventSerializer>();
			container.RegisterType<IStoredEventRepository, StoredEventRepository>();
			container.RegisterType<DomainEvents, DomainEvents>();

			container.RegisterType<IEventForwarderService, EventForwarderService>();
			container.RegisterType<IMessagingService, AzureServiceBusQueueMessagingService>();
			container.RegisterType<IProductDeletedEventConsumer, ProductDeletedEventConsumer>();
		}

		public static void RegisterEventsSubscribers()
		{
			_container.Resolve<DomainEvents>();

			var eventStoringSubscriber = _container.Resolve<IEventStoringSubscriber>();			
			DomainEvents.Publisher.Subscribe(eventStoringSubscriber);
		}
    }
}