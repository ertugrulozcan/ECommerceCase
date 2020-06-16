using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;

namespace ECommerceCase.OMS.Services.Interfaces
{
	public interface IWarehouseService
	{
		IResponseResult<IEnumerable<Product>> GetStockWares(int brandId);

		Task<IResponseResult<IEnumerable<Product>>> GetStockWaresAsync(int brandId);
	}
}