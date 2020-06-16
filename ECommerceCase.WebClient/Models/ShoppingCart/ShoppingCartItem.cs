using ECommerceCase.Core.Models.Products;

namespace ECommerceCase.WebClient.Models.ShoppingCart
{
	public class ShoppingCartItem
	{
		#region Properties

		public Product Product { get; set; }
		
		public int Quantity { get; set; }

		public double TotalPrice
		{
			get
			{
				if (this.Product == null)
					return 0.0d;

				return this.Product.Price.Value * this.Quantity;
			}
		}

		#endregion
	}
}