using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;

namespace ECommerceCase.PIM.Services.Interfaces
{
	public interface IProductsService
	{
		Product GetProduct(int id);
		
		IEnumerable<Product> GetProducts();
		
		IEnumerable<Product> GetProducts(int? skip, int? limit, out int totalCount);

		IEnumerable<Product> GetProductsByCategory(int categoryId, int? skip, int? limit, out int totalCount);
		
		BrandModel GetBrandById(int brandId);
	}
}