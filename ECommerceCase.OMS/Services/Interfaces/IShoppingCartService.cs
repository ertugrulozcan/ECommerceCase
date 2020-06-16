using ECommerceCase.Core.Models.Shopping;

namespace ECommerceCase.OMS.Services.Interfaces
{
	public interface IShoppingCartService
	{
		ShoppingCart GetShoppingCart(string userId);
		
		ShoppingCart AddToShoppingCart(string userId, int productId);
		
		ShoppingCart RemoveFromShoppingCart(string userId, int productId);
		
		ShoppingCart EmptyShoppingCart(string userId);
	}
}