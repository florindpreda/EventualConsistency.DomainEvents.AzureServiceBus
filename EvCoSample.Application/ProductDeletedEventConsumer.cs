using EvCoSample.Core;
using EvCoSample.Domain;
using EvCoSample.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCoSample.Application
{
	public class ProductDeletedEventConsumer : IProductDeletedEventConsumer
	{
		private readonly string QUEUE_NAME = "ProductDeletedQueue";
		private readonly IMessagingService _messagingService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IProductReviewRepository _productReviewRepository;

		public ProductDeletedEventConsumer(IMessagingService messagingService, IUnitOfWork unitOfWork, IProductReviewRepository productReviewRepository)
		{
			_messagingService = messagingService;
			_unitOfWork = unitOfWork;
			_productReviewRepository = productReviewRepository;
		}

		public void ProcessNextEvent()
		{
			_messagingService.ProcessNextEvent<ProductDeleted>(pd => Process(pd), QUEUE_NAME);
		}

		private void Process(ProductDeleted productDeleted)
		{
			using(_unitOfWork)
			{
				var productReviews = _productReviewRepository.GetByProductId(productDeleted.ProductId);
				foreach (var productReview in productReviews)
				{
					productReview.Delete();
					_productReviewRepository.Update(productReview);
				}

				_unitOfWork.Commit();
			}
		}
	}
}
