using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.PIM.Services.Interfaces;

namespace ECommerceCase.PIM.Services
{
	public class CategoryService : ICategoryService
	{
		#region Services

		private readonly IRepository<EmtiaCategory> categoryRepository;
		private readonly IRepository<ProductKind> productKindRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="categoryRepository"></param>
		/// <param name="productKindRepository"></param>
		public CategoryService(IRepository<EmtiaCategory> categoryRepository, IRepository<ProductKind> productKindRepository)
		{
			this.categoryRepository = categoryRepository;
			this.productKindRepository = productKindRepository;
		}

		#endregion
		
		#region Methods

		public EmtiaCategory GetCategoryById(int id)
		{
			return this.categoryRepository.Select(id);
		}

		public ProductKind GetProductKindById(int id)
		{
			return this.productKindRepository.Select(id);
		}

		public IEnumerable<EmtiaCategory> GetCategories()
		{
			return this.categoryRepository.Select();
		}
		
		#endregion
	}
}