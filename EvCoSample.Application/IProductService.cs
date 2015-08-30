using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Application.Commands;

namespace EvCoSample.Application
{
    public interface IProductService
    {
		void Delete(DeleteProductCommand command);
    }
}
