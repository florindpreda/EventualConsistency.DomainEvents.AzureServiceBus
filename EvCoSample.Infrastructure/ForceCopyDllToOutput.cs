using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Infrastructure
{
	internal static class ForceCopyDllToOutput
	{
		private static SqlProviderServices instance = SqlProviderServices.Instance;		
	}
}