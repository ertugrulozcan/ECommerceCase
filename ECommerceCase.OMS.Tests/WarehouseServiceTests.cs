using System.Collections.Generic;
using ECommerceCase.OMS.Services;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace ECommerceCase.OMS.Tests
{
	public class WarehouseServiceTests
	{
		#region Services

		private IWarehouseService _warehouseService;

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
			
			this._warehouseService = new WarehouseService(configuration);
		}

		#endregion
		
		#region Methods

		[Test]
		public void GetProductByIdTest()
		{
			var getProductResponse = this._warehouseService.GetStockWares(100);
			Assert.IsTrue(getProductResponse.IsSuccess);
		}

		#endregion
	}
}