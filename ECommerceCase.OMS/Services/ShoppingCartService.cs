using System.Collections.Generic;
using System.Linq;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Models.Shopping;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.OMS.Services.Interfaces;

namespace ECommerceCase.OMS.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		#region Services

		private readonly IProductService productService;
		private readonly IWarehouseService warehouseService;
		private readonly IRepository<ShoppingCart> shoppingCartRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="productService"></param>
		/// <param name="warehouseService"></param>
		/// <param name="shoppingCartRepository"></param>
		public ShoppingCartService(IProductService productService, IWarehouseService warehouseService, IRepository<ShoppingCart> shoppingCartRepository)
		{
			this.productService = productService;
			this.warehouseService = warehouseService;
			this.shoppingCartRepository = shoppingCartRepository;
		}

		#endregion
		
		#region Methods

		public ShoppingCart GetShoppingCart(string userId)
		{
			return this.shoppingCartRepository.Where(x => x.UserId == userId).FirstOrDefault();
		}

		public ShoppingCart AddToShoppingCart(string userId, int productId)
		{
			var getProductResponse = this.productService.GetProductById(productId);
			if (getProductResponse.IsSuccess)
			{
				var product = getProductResponse.Data;
				
				// Is exist in stock ??
				var waresResponse = this.warehouseService.GetStockWares(product.Brand.Id);
				if (waresResponse.IsSuccess)
				{
					if (waresResponse.Data.Any(x => x.Id == productId))
					{
						var shoppingCart = this.GetShoppingCart(userId);
						if (shoppingCart != null)
						{
							shoppingCart.CartItems.Add(product);
							this.shoppingCartRepository.Update(shoppingCart);

							return shoppingCart;
						}
						else
						{
							shoppingCart = new ShoppingCart
							{
								UserId = userId,
								CartItems = new List<Product>(new[] { product })
							};
				
							this.shoppingCartRepository.Insert(shoppingCart);
							return shoppingCart;
						}
					}
					else
					{
						// Not exist in stock
						return null;
					}
				}
				else
				{
					// Warehouse response fail
					return null;
				}	
			}
			else
			{
				// Product response fail
				return null;
			}
		}

		public ShoppingCart RemoveFromShoppingCart(string userId, int productId)
		{
			var shoppingCart = this.GetShoppingCart(userId);
			if (shoppingCart != null)
			{
				var product = shoppingCart.CartItems.FirstOrDefault(x => x.Id == productId);
				if (product != null)
				{
					shoppingCart.CartItems.Remove(product);
					this.shoppingCartRepository.Update(shoppingCart);

					return shoppingCart;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public ShoppingCart EmptyShoppingCart(string userId)
		{
			var shoppingCart = this.GetShoppingCart(userId);
			if (shoppingCart != null)
			{
				shoppingCart.CartItems.Clear();
				this.shoppingCartRepository.Update(shoppingCart);

				return shoppingCart;
			}
			else
			{
				return null;
			}
		}
		
		#endregion
	}
}