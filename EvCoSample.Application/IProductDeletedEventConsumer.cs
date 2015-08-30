using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvCoSample.Application
{
	public interface IProductDeletedEventConsumer
	{
		void ProcessNextEvent();
	}
}