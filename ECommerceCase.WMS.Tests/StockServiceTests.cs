using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.WMS.Dao.MockDB;
using ECommerceCase.WMS.Services;
using ECommerceCase.WMS.Services.Interfaces;
using NUnit.Framework;

namespace ECommerceCase.WMS.Tests
{
	public class StockServiceTests
	{
		#region Services

		private IStockService _stockService;

		#endregion
		
		#region Setup
		
		[SetUp]
		public void Setup()
		{
			IRepository<Product> stockRepository = new StockRepository();
			stockRepository.Insert(new Product
			{
				Id = 1001,
				Name = "UnitTestProduct",
				Brand = new BrandModel
				{
					Id = 1000,
					Name = "UnitTestBrand"
				},
				Category = new EmtiaCategory
				{
					Id = 100,
					Name = "UnitTestCategory"
				},
				Kind = new ProductKind
				{
					Id = 1000,
					Name = "UnitTestKind"
				},
				ModelId = 5001
			});
			
			this._stockService = new StockService(stockRepository);
		}

		#endregion
		
		#region Methods

		[Test]
		public void GetWaresByBrandIdTest()
		{
			var wares = this._stockService.GetWaresByBrandId(1000);
			Assert.NotNull(wares);
		}
		
		[Test]
		public void GetWaresByModelIdTest()
		{
			var wares = this._stockService.GetWaresByBrandId(5001);
			Assert.NotNull(wares);
		}

		#endregion
	}
}