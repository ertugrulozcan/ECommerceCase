using System.Collections.Generic;
using System.Linq;
using ECommerceCase.OMS.Dao.MockDB;
using ECommerceCase.OMS.Services;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ECommerceCase.OMS.Tests
{
	public class ShoppingCartServiceTests
	{
		#region Services

		private IShoppingCartService _shoppingCartService;

		#endregion
		
		#region Setup

		[SetUp]
		public void Setup()
		{
			var myConfiguration = new Dictionary<string, string>
			{
				{ "PimApiUrl", "http://localhost:9000" },
				{ "WmsApiUrl", "http://localhost:8000" }
			};

			var configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(myConfiguration)
				.Build();
			
			var productService = new ProductService(configuration);
			var warehouseService = new WarehouseService(configuration);
			var shoppingCartRepository = new ShoppingCartRepository();
			
			this._shoppingCartService = new ShoppingCartService(productService, warehouseService, shoppingCartRepository);
		}

		#endregion
		
		#region Methods

		[Test]
		public void GetShoppingCartTest()
		{
			var shoppingCart = this._shoppingCartService.GetShoppingCart("5ee72e69444c9c927de01342");
			Assert.IsNotNull(shoppingCart);
		}
		
		[Test]
		public void AddToShoppingCartTest()
		{
			var shoppingCart = this._shoppingCartService.AddToShoppingCart("5ee72e69444c9c927de01342", 1005);
			Assert.IsNotNull(shoppingCart);
			
			Assert.IsTrue(shoppingCart.CartItems.Any(x => x.Id == 1005));
		}
		
		[Test]
		public void EmptyShoppingCartTest()
		{
			var shoppingCart = this._shoppingCartService.EmptyShoppingCart("5ee72e69444c9c927de01342");
			Assert.IsNotNull(shoppingCart);
			
			Assert.IsFalse(shoppingCart.CartItems.Any());
		}

		#endregion
	}
}