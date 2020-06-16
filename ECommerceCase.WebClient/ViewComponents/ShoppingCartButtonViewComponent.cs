using System.Linq;
using System.Threading.Tasks;
using ECommerceCase.WebClient.Services.Interfaces;
using ECommerceCase.WebClient.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.WebClient.ViewComponents
{
	public class ShoppingCartButtonViewComponent : ViewComponent
	{
		#region Services

		private readonly IShoppingService shoppingService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="shoppingService"></param>
		public ShoppingCartButtonViewComponent(IShoppingService shoppingService)
		{
			this.shoppingService = shoppingService;
		}

		#endregion
		
		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			ShoppingCartButtonViewModel viewModel = new ShoppingCartButtonViewModel();

			var userId = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			var getShoppingCartItemsResponse = await this.shoppingService.GetShoppingCartItemsAsync(userId);
			if (getShoppingCartItemsResponse.IsSuccess)
			{
				viewModel.ShoppingCartItems = getShoppingCartItemsResponse.Data.ToList();
			}
			
			return this.View(viewModel);
		}
		
		#endregion
	}
}