using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace EvCoSample.API
{
    public static class UnityConfig
    {
		private static UnityContainer _container = new UnityContainer();

        public static void RegisterComponents()
        {
			EvCoSample.DependencyResolver.UnityConfig.RegisterComponents(_container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(_container);
        }

		public static void RegisterEventsSubscribers()
		{
			EvCoSample.DependencyResolver.UnityConfig.RegisterEventsSubscribers();
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