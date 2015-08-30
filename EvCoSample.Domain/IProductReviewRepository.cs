using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Domain
{
	public interface IProductReviewRepository
	{
		IEnumerable<ProductReview> GetByProductId(Guid productId);
		void Update(ProductReview productReview);
	}
}