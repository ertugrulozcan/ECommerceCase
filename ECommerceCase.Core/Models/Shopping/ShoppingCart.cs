using System.Collections.Generic;
using ECommerceCase.Core.Models.Products;

namespace ECommerceCase.Core.Models.Shopping
{
	public class ShoppingCart : EntityBase
	{
		#region Properties

		public string UserId { get; set; }
		
		public List<Product> CartItems { get; set; } = new List<Product>();

		#endregion
	}
}