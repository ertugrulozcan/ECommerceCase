using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.PIM.Dao.MockDB;
using ECommerceCase.PIM.Services;
using ECommerceCase.PIM.Services.Interfaces;
using NUnit.Framework;

namespace ECommerceCase.PIM.Tests
{
	public class CategoryServiceTests
	{
		#region Services

		private ICategoryService _categoryService;

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
			
			this._categoryService = new CategoryService(categoryRepository, productKindRepository);
		}

		#endregion
		
		#region Methods

		[Test]
		public void GetCategoryByIdTest()
		{
			var category = this._categoryService.GetCategoryById(100);
			Assert.NotNull(category);
		}

		[Test]
		public void GetProductKindByIdTest()
		{
			var kind = this._categoryService.GetProductKindById(1000);
			Assert.NotNull(kind);
		}

		[Test]
		public void GetCategoriesTest()
		{
			var categories = this._categoryService.GetCategories();
			Assert.NotNull(categories);
		}

		#endregion
	}
}