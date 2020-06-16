using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;

namespace ECommerceCase.OMS.Services.Interfaces
{
	public interface IProductService
	{
		IResponseResult<Product> GetProductById(int productId);

		Task<IResponseResult<Product>> GetProductByIdAsync(int productId);
	}
}