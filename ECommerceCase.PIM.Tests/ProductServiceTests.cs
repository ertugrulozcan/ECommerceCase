using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.PIM.Dao.MockDB;
using ECommerceCase.PIM.Services;
using ECommerceCase.PIM.Services.Interfaces;
using NUnit.Framework;

namespace ECommerceCase.PIM.Tests
{
	public class ProductServiceTests
	{
		#region Services

		private IProductsService _productService;

		#endregion
		
		#region Setup
		
		[SetUp]
		public void Setup()
		{
			IRepository<EmtiaCategory> categoryRepository = new CategoryRepository();
			categoryRepository.Insert(new EmtiaCategory
			{
				Id = 100,
				Name = "UnitTestCategory"
			});
			
			IRepository<ProductKind> productKindRepository = new ProductKindRepository();
			productKindRepository.Insert(new ProductKind
			{
				Id = 1000,
				Name = "UnitTestKind"
			});
			
			ICategoryService categoryService = new CategoryService(categoryRepository, productKindRepository);
			
			IRepository<Product> productRepository = new ProductsRepository(categoryService);
			productRepository.Insert(new Product
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
			
			IRepository<BrandModel> brandRepository = new BrandRepository();
			brandRepository.Insert(new BrandModel
			{
				Id = 1000,
				Name = "UnitTestBrand"
			});
			
			this._productService = new ProductsService(productRepository, brandRepository);
		}

		#endregion
		
		#region Methods

		[Test]
		public void GetProductTest()
		{
			var product = this._productService.GetProduct(1001);
			Assert.NotNull(product);
		}
		
		[Test]
		public void GetProductsTest()
		{
			var products = this._productService.GetProducts();
			Assert.NotNull(products);
		}
		
		[Test]
		public void GetProductsByCategoryTest()
		{
			var products = this._productService.GetProductsByCategory(100, 0, 5, out int _);
			Assert.NotNull(products);
		}

		[Test]
		public void GetBrandByIdTest()
		{
			var brand = this._productService.GetBrandById(1000);
			Assert.NotNull(brand);
		}

		#endregion
	}
}