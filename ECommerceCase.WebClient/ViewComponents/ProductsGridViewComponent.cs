using System.Linq;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.WebClient.Extensions;
using ECommerceCase.WebClient.Services.Interfaces;
using ECommerceCase.WebClient.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.WebClient.ViewComponents
{
	public class ProductsGridViewComponent : ViewComponent
	{
		#region Services

		private readonly IProductService productService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="productService"></param>
		public ProductsGridViewComponent(IProductService productService)
		{
			this.productService = productService;
		}

		#endregion
		
		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ProductsGridViewModel viewModel = new ProductsGridViewModel();
			var getProductsResponse = await productService.GetProductsAsync();
			if (getProductsResponse.IsSuccess)
			{
				var getStockResponse = await productService.GetStockWaresAsync(100);
				if (getStockResponse.IsSuccess)
				{
					var products = getProductsResponse.Data;
					viewModel.Cards = products.Select(
						x => x.ConvertToProductCard(getStockResponse.Data.Any(
							y => y.ModelId == x.ModelId) ? 
							SaleStatus.Sale : 
							SaleStatus.OutOfStock))
						.ToList();	
				}
				else
				{
					viewModel.ModelStateErrorMessage = getStockResponse.Message;
				}
			}
			else
			{
				viewModel.ModelStateErrorMessage = getProductsResponse.Message;
			}
			
			return this.View(viewModel);
		}
		
		#endregion
	}
}