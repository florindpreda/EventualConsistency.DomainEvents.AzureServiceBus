using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.EventForwarderWorker.Startup;
using Microsoft.Practices.Unity;
using EvCoSample.Core;
using System.Threading;

namespace EvCoSample.EventForwarderWorker
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