using EvCoSampleProductDeletedConsumerWorker.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace EvCoSampleProductDeletedConsumerWorker
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				UnityConfig.RegisterComponents();				

				var worker = UnityConfig.Container.Resolve<Worker>();
				worker.Run();

				Thread.Sleep(5000);
			}
		}
	}
}