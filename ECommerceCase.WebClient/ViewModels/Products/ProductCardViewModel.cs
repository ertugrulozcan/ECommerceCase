using ECommerceCase.Core.Models.Monetary;
using ECommerceCase.Core.Models.Products;

namespace ECommerceCase.WebClient.ViewModels.Products
{
	public class ProductCardViewModel
	{
		#region Properties

		public int ProductId { get; set; }
		
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		public string ImageUrl { get; set; }
		
		public PriceModel Price { get; set; }
		
		public SaleStatus SaleStatus { get; set; }

		#endregion
	}
}