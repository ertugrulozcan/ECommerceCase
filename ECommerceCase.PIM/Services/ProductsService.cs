using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.PIM.Services.Interfaces;

namespace ECommerceCase.PIM.Services
{
	public class ProductsService : IProductsService
	{
		#region Services

		private readonly IRepository<Product> productRepository;
		private readonly IRepository<BrandModel> brandRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="productRepository"></param>
		/// <param name="brandRepository"></param>
		public ProductsService(IRepository<Product> productRepository, IRepository<BrandModel> brandRepository)
		{
			this.productRepository = productRepository;
			this.brandRepository = brandRepository;
		}

		#endregion
		
		#region Methods

		public Product GetProduct(int id)
		{
			return this.productRepository.Select(id);
		}
		
		public IEnumerable<Product> GetProducts()
		{
			return this.productRepository.Select();
		}
		
		public IEnumerable<Product> GetProducts(int? skip, int? limit, out int totalCount)
		{
			return this.productRepository.Select(skip, limit, out totalCount);
		}
		
		public IEnumerable<Product> GetProductsByCategory(int categoryId, int? skip, int? limit, out int totalCount)
		{
			return this.productRepository.Where(x => x.Category != null && x.Category.Id == categoryId, skip, limit, out totalCount);
		}

		public BrandModel GetBrandById(int brandId)
		{
			return this.brandRepository.Select(brandId);
		}

		#endregion
	}
}