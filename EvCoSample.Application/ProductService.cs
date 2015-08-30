using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvCoSample.Application.Commands;
using EvCoSample.Core;
using EvCoSample.Domain;

namespace EvCoSample.Application
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductRepository _productRepository;
		private readonly IProductReviewRepository _productReviewRepository;

		public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IProductReviewRepository productReviewRepository)
		{
			_unitOfWork = unitOfWork;
			_productRepository = productRepository;
			_productReviewRepository = productReviewRepository;
		}

		public void Delete(DeleteProductCommand command)
		{
			using (_unitOfWork)
			{
				var product = _productRepository.GetById(command.Id);
				product.Delete();
				_productRepository.Update(product);				

				_unitOfWork.Commit();
			}			
		}
	}
}