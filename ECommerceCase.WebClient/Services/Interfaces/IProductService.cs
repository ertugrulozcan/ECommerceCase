using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;

namespace ECommerceCase.WebClient.Services.Interfaces
{
	public interface IProductService
	{
		IResponseResult<IEnumerable<Product>> GetProducts();
		
		Task<IResponseResult<IEnumerable<Product>>> GetProductsAsync();
		
		IResponseResult<IEnumerable<Product>> GetStockWares(int brandId);
		
		Task<IResponseResult<IEnumerable<Product>>> GetStockWaresAsync(int brandId);
	}
}