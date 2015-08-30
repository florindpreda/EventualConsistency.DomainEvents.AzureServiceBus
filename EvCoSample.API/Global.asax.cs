using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EvCoSample.Core;
using Microsoft.Practices.Unity;
using EvCoSample.Core.Events;

namespace EvCoSample.API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			
			//IoC configurations
			UnityConfig.RegisterComponents();

			GlobalConfiguration.Configure(WebApiConfig.Register);									
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			//creates and seeds the database at first run
			UnityConfig.Container.Resolve<IDatabaseConfiguration>().Initialise();
		}

		protected void Application_BeginRequest()
		{
			UnityConfig.RegisterEventsSubscribers();
		}
	}
}
