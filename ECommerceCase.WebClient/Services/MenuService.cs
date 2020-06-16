using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Models.NavigationMenu;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Services
{
	public class MenuService : IMenuService
	{
		#region Services

		private readonly ILogger<MenuService> _logger;
		private readonly ICategoryService categoryService;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="categoryService"></param>
		/// <param name="logger"></param>
		public MenuService(ICategoryService categoryService, ILogger<MenuService> logger)
		{
			this.categoryService = categoryService;
			this._logger = logger;
		}

		#endregion
		
		#region Methods

		public IResponseResult<IEnumerable<IMenuItem>> GetNavigationMenu()
		{
			return this.GetNavigationMenuAsync().ConfigureAwait(false).GetAwaiter().GetResult();
		}
		
		public async Task<IResponseResult<IEnumerable<IMenuItem>>> GetNavigationMenuAsync()
		{
			var getCategoriesResponse = await this.categoryService.GetCategoriesAsync();
			if (getCategoriesResponse.IsSuccess)
			{
				var categories = getCategoriesResponse.Data;
				
				List<IMenuItem> menuItems = new List<IMenuItem>();
				foreach (var category in categories)
				{
					menuItems.Add(this.ConvertFromCategory(category));
				}

				return new ResponseResult<IEnumerable<IMenuItem>>(true) { Data = menuItems };
			}
			else
			{
				this._logger.Log(LogLevel.Critical, getCategoriesResponse.Message);
				return new ResponseResult<IEnumerable<IMenuItem>>(false, getCategoriesResponse.Message);
			}
		}

		private IMenuItem ConvertFromCategory(EmtiaCategory category)
		{
			var subMenuItems = category.SubCategories?.Select(this.ConvertFromCategory);
			return new MenuItem
			{
				Title = category.Name,
				Href = subMenuItems != null && subMenuItems.Any() ? "javascript:void(0)" : "#",
				SubItems = subMenuItems?.ToList()
			};
		}
		
		#endregion
	}
}