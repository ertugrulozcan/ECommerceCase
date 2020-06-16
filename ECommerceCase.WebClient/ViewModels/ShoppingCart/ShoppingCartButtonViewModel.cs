using System.Collections.Generic;
using ECommerceCase.WebClient.Models.ShoppingCart;

namespace ECommerceCase.WebClient.ViewModels.ShoppingCart
{
	public class ShoppingCartButtonViewModel : ViewModelBase
	{
		#region Propreties

		public IList<ShoppingCartItem> ShoppingCartItems { get; set; }

		#endregion
	}
}