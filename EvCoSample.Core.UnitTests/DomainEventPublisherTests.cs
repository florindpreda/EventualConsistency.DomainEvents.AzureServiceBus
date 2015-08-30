using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvCoSample.Core.Events;

namespace EvCoSample.Core.UnitTests
{
	[TestClass]
	public class DomainEventPublisherTests
	{
		[TestMethod]
		public void Subscribe_Handle_Adds_SubscriberWithHandle()
		{
			//arrange
			var publisher = new DomainEventPublisher();
			Action<DomainEvent> handle = domainEvent => { };

			//act
			publisher.Subscribe<DomainEvent>(handle);

			//assert
		}

		[TestMethod]
		public void Subscribe_DomainEventSubscriber_Adds_DomainEventSubscriber()
		{
			//arrange
			var publisher = new DomainEventPublisher();
			Action<DomainEvent> handle = domainEvent => { };
			var domainEventSubscriber = new DomainEventSubscriber(handle, typeof(DomainEvent));

			//act
			publisher.Subscribe(domainEventSubscriber);
			
			//assert
		}

		[TestMethod]
		public void Publish_ExecutesHandleForEvent_When_ThereIsOneSpecificSubscriber()
		{
			//arrange			
			var specificHandlerExecuted = false;
			var publisher = new DomainEventPublisher();
			var testDomainEvent = new TestDomainEvent();
			
			Action<DomainEvent> testDomainEventHandle = testDomainEventParam => 
			{ 
				specificHandlerExecuted = true; 
			};
			
			publisher.Subscribe<TestDomainEvent>(testDomainEventHandle);
			
			//act
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());
			
			//assert
			Assert.AreEqual(specificHandlerExecuted, true);			
		}

		[TestMethod]
		public void Publish_ExecutesHandleForEvent_When_ThereIsOneGeneralSubscriber()
		{
			//arrange
			var generalHandlerExecuted = false;			
			var publisher = new DomainEventPublisher();
			var testDomainEvent = new TestDomainEvent();			
			Action<DomainEvent> domainEventHandle = domainEventParam =>
			{
				generalHandlerExecuted = true;
			};

			publisher.Subscribe<DomainEvent>(domainEventHandle);

			//act
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());

			//assert
			Assert.AreEqual(true, generalHandlerExecuted);			
		}

		[TestMethod]
		public void Publish_ExecutesOnlyGeneralHandlersForEvent_When_ThereIsAMixOfGeneralSubscribersAndSpecificSubscribersForOtherEvents()
		{
			//arrange
			var generalHandlerExecuted1 = false;
			var generalHandlerExecuted2 = false;
			var specificHandlerExecuted1 = false;
			var specificHandlerExecuted2 = false;
			var publisher = new DomainEventPublisher();
			var testDomainEvent = new TestDomainEvent();
			Action<DomainEvent> generalEventHandle1 = domainEventParam =>
			{
				generalHandlerExecuted1 = true;
			};
			Action<DomainEvent> generalEventHandle2 = domainEventParam =>
			{
				generalHandlerExecuted2 = true;
			};
			Action<DomainEvent> specificEventHandle1 = domainEventParam =>
			{
				specificHandlerExecuted1 = true;
			};
			Action<DomainEvent> specificEventHandle2 = domainEventParam =>
			{
				specificHandlerExecuted2 = true;
			};

			publisher.Subscribe<DomainEvent>(generalEventHandle1);
			publisher.Subscribe<DomainEvent>(generalEventHandle2);
			publisher.Subscribe<TestDomainEvent2>(specificEventHandle1);
			publisher.Subscribe<TestDomainEvent2>(specificEventHandle2);
			
			//act			
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());			

			//assert
			Assert.AreEqual(true, generalHandlerExecuted1);
			Assert.AreEqual(true, generalHandlerExecuted2);
			Assert.AreEqual(false, specificHandlerExecuted1);
			Assert.AreEqual(false, specificHandlerExecuted2);
		}

		[TestMethod]
		public void Publish_ExecutesOnlySpecificAndGeneralHandlersForEvent_When_ThereIsAMixOfGeneralSubscribersAndSpecificSubcribersForThatEventAndSpecificSubscribersForOtherEvents()
		{
			//arrange
			var generalHandlerExecuted1 = false;
			var generalHandlerExecuted2 = false;
			var specificHandlerExecuted1 = false;
			var specificHandlerExecuted2 = false;
			var specificOtherHandlerExecuted1 = false;
			var specificOtherHandlerExecuted2 = false;
			var publisher = new DomainEventPublisher();
			var testDomainEvent = new TestDomainEvent();
			Action<DomainEvent> generalEventHandle1 = domainEventParam =>
			{
				generalHandlerExecuted1 = true;
			};
			Action<DomainEvent> generalEventHandle2 = domainEventParam =>
			{
				generalHandlerExecuted2 = true;
			};
			Action<DomainEvent> specificEventHandle1 = domainEventParam =>
			{
				specificHandlerExecuted1 = true;
			};
			Action<DomainEvent> specificEventHandle2 = domainEventParam =>
			{
				specificHandlerExecuted2 = true;
			};
			Action<DomainEvent> specificOtherEventHandle1 = domainEventParam =>
			{
				specificOtherHandlerExecuted1 = true;
			};
			Action<DomainEvent> specificOtherEventHandle2 = domainEventParam =>
			{
				specificOtherHandlerExecuted2 = true;
			};

			publisher.Subscribe<DomainEvent>(generalEventHandle1);
			publisher.Subscribe<DomainEvent>(generalEventHandle2);
			publisher.Subscribe<TestDomainEvent>(specificEventHandle1);
			publisher.Subscribe<TestDomainEvent>(specificEventHandle2);
			publisher.Subscribe<TestDomainEvent2>(specificOtherEventHandle1);
			publisher.Subscribe<TestDomainEvent2>(specificOtherEventHandle2);

			//act
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());

			//assert
			Assert.AreEqual(true, generalHandlerExecuted1);
			Assert.AreEqual(true, generalHandlerExecuted2);
			Assert.AreEqual(true, specificHandlerExecuted1);
			Assert.AreEqual(true, specificHandlerExecuted2);
			Assert.AreEqual(false, specificOtherHandlerExecuted1);
			Assert.AreEqual(false, specificOtherHandlerExecuted2);
		}

		[TestMethod]
		public void Publish_ExecutesMultipleHandlesForEvent_When_ThereIsAMixOfSpecificSubscribersForThatEventAndGeneralSubscribers()
		{
			//arrange
			var generalHandlerExecuted1 = false;
			var generalHandlerExecuted2 = false;
			var specificHandlerExecuted1 = false;
			var specificHandlerExecuted2 = false;
			var publisher = new DomainEventPublisher();			

			Action<DomainEvent> generalEventHandle1 = domainEventParam =>
			{
				generalHandlerExecuted1 = true;
			};

			Action<DomainEvent> generalEventHandle2 = domainEventParam =>
			{
				generalHandlerExecuted2 = true;
			};

			Action<DomainEvent> specificEventHandle1 = domainEventParam =>
			{
				specificHandlerExecuted1 = true;
			};

			Action<DomainEvent> specificEventHandle2 = domainEventParam =>
			{
				specificHandlerExecuted2 = true;
			};

			publisher.Subscribe<DomainEvent>(generalEventHandle1);
			publisher.Subscribe<DomainEvent>(generalEventHandle2);
			publisher.Subscribe<TestDomainEvent>(specificEventHandle1);
			publisher.Subscribe<TestDomainEvent>(specificEventHandle2);

			//act					
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());

			//assert
			Assert.AreEqual(true, generalHandlerExecuted1);
			Assert.AreEqual(true, generalHandlerExecuted2);
			Assert.AreEqual(true, specificHandlerExecuted1);
			Assert.AreEqual(true, specificHandlerExecuted2);
		}

		[TestMethod]
		public void Publish_DoesNotExecuteHandlersForASpecificEvent_When_ThereAreNoGeneralSubscribersAndNoSpecificSubscribersForThatEvent()
		{
			//arrange			
			var specificHandlerExecuted1 = false;
			var specificHandlerExecuted2 = false;
			var publisher = new DomainEventPublisher();			

			Action<DomainEvent> specificEventHandle1 = domainEventParam =>
			{
				specificHandlerExecuted1 = true;
			};

			Action<DomainEvent> specificEventHandle2 = domainEventParam =>
			{
				specificHandlerExecuted2 = true;
			};
			
			publisher.Subscribe<TestDomainEvent2>(specificEventHandle1);
			publisher.Subscribe<TestDomainEvent2>(specificEventHandle2);

			//act
			publisher.Publish<TestDomainEvent>(new TestDomainEvent());

			//assert			
			Assert.AreEqual(false, specificHandlerExecuted1);
			Assert.AreEqual(false, specificHandlerExecuted2);
		}

		#region test supporting classes
		class TestDomainEvent : DomainEvent { }
		class TestDomainEvent2 : DomainEvent { }		
		#endregion
	}		
}