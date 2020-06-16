using System.Collections.Generic;
using ECommerceCase.Core.Models.Products;

namespace ECommerceCase.WMS.Services.Interfaces
{
	public interface IStockService
	{
		IEnumerable<Product> GetWaresByBrandId(int brandId);
		
		IEnumerable<Product> GetWaresByModelId(int modelId);
	}
}