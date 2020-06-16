using System.Threading.Tasks;
using ECommerceCase.WebClient.Services.Interfaces;
using ECommerceCase.WebClient.ViewModels.Menu;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.WebClient.ViewComponents
{
	public class NavigationMenuViewComponent : ViewComponent
	{
		#region Services

		private readonly IMenuService menuService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="menuService"></param>
		public NavigationMenuViewComponent(IMenuService menuService)
		{
			this.menuService = menuService;
		}

		#endregion
		
		#region Methods

		public async Task<IViewComponentResult> InvokeAsync()
		{
			NavigationMenuViewModel viewModel = new NavigationMenuViewModel();
			var getMenuResponse = await menuService.GetNavigationMenuAsync();
			if (getMenuResponse.IsSuccess)
			{
				viewModel.Menu = getMenuResponse.Data;
			}
			else
			{
				viewModel.ModelStateErrorMessage = getMenuResponse.Message;
			}
			
			return this.View(viewModel);
		}
		
		#endregion
	}
}