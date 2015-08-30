using EvCoSample.Application;
using EvCoSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSampleProductDeletedConsumerWorker
{
	public class Worker : IWorker
	{
		private readonly IProductDeletedEventConsumer _productDeletedEventConsumer;

		public Worker(IProductDeletedEventConsumer productDeletedEventConsumer)
		{
			_productDeletedEventConsumer = productDeletedEventConsumer;
		}

		public void Run()
		{
			_productDeletedEventConsumer.ProcessNextEvent();
		}
	}
}
