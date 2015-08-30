using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Application;
using EvCoSample.Application.Commands;
using EvCoSample.Core;

namespace EvCoSample.EventForwarderWorker
{
	public class Worker : IWorker
	{
		private readonly IEventForwarderService _eventForwarderService;

		public Worker(IEventForwarderService eventForwarderService)
		{
			_eventForwarderService = eventForwarderService;
		}

		public void Run()
		{
			_eventForwarderService.ForwardEvents();
		}
	}
}
