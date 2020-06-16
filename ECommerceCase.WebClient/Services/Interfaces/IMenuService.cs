using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Models.NavigationMenu;

namespace ECommerceCase.WebClient.Services.Interfaces
{
	public interface IMenuService
	{
		IResponseResult<IEnumerable<IMenuItem>> GetNavigationMenu();

		Task<IResponseResult<IEnumerable<IMenuItem>>> GetNavigationMenuAsync();
	}
}