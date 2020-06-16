using System.Linq;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.WebClient.ViewModels.Products;

namespace ECommerceCase.WebClient.Extensions
{
	public static class ProductExtensions
	{
		public static ProductCardViewModel ConvertToProductCard(this Product product, SaleStatus saleStatus)
		{
			return new ProductCardViewModel
			{
				ProductId = product.Id,
				Title = product.Name,
				Description = product.Description,
				ImageUrl = product.Images?.FirstOrDefault(),
				Price = product.Price,
				SaleStatus = saleStatus
			};
		}
	}
}