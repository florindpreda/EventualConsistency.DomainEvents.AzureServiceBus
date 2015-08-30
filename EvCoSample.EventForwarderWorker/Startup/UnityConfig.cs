using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.EventForwarderWorker.Startup
{
	public static class UnityConfig
	{
		private static UnityContainer _container = new UnityContainer();

		public static void RegisterComponents()
		{
			EvCoSample.DependencyResolver.UnityConfig.RegisterComponents(_container);		
		}

		public static UnityContainer Container
		{
			get
			{
				return _container;
			}
		}
	}
}
