using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Models.ShoppingCart;

namespace ECommerceCase.WebClient.Services.Interfaces
{
	public interface IShoppingService
	{
		IResponseResult<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(string userId);

		Task<IResponseResult<IEnumerable<ShoppingCartItem>>> GetShoppingCartItemsAsync(string userId);
		
		IResponseResult AddToShoppingCart(string userId, in int productId);

		Task<IResponseResult> AddToShoppingCartAsync(string userId, int productId);

		IResponseResult RemoveFromShoppingCart(string userId, int productId);

		Task<IResponseResult> RemoveFromShoppingCartAsync(string userId, int productId);
		
		IResponseResult EmptyShoppingCart(string userId);

		Task<IResponseResult> EmptyShoppingCartAsync(string userId);
	}
}