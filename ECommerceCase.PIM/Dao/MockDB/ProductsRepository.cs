using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Monetary;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao.MockDB;
using ECommerceCase.PIM.Services.Interfaces;

namespace ECommerceCase.PIM.Dao.MockDB
{
	public sealed class ProductsRepository : RepositoryBase<Product>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public ProductsRepository(ICategoryService categoryService)
		{
			BrandModel mockBrand = new BrandModel
			{
				Id = 100,
				Name = "BlueJeans"
			};
			
			this.Insert(new []
			{
				new Product
				{
					Id = 10001,
					Name = "Ripped Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans1.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5001,
					Price = new PriceModel(24.99d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10002,
					Name = "Mega Ripped Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans2.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5002,
					Price = new PriceModel(19.99d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10003,
					Name = "Washed Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans3.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5003,
					Price = new PriceModel(20.50d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10004,
					Name = "Vintage Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans4.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5004,
					Price = new PriceModel(29.99d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10005,
					Name = "Mega Ripped Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans2.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5002,
					Price = new PriceModel(19.99d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10006,
					Name = "Vintage Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans4.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5004,
					Price = new PriceModel(29.99d, CurrencyModel.TRY)
				},new Product
				{
					Id = 10007,
					Name = "Ripped Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans1.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5001,
					Price = new PriceModel(24.99d, CurrencyModel.TRY)
				},
				new Product
				{
					Id = 10008,
					Name = "Washed Skinny Jeans",
					Description = "",
					Images = new [] { "https://www.w3schools.com/w3images/jeans3.jpg" },
					Kind = categoryService.GetProductKindById(100),
					Category = categoryService.GetCategoryById(301),
					Brand = mockBrand,
					ModelId = 5003,
					Price = new PriceModel(20.50d, CurrencyModel.TRY)
				},
			});
		}

		#endregion
	}
}