using System.Collections.Generic;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.WMS.Services.Interfaces;

namespace ECommerceCase.WMS.Services
{
	public class StockService : IStockService
	{
		#region Services

		private readonly IRepository<Product> stockRepository;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="stockRepository"></param>
		public StockService(IRepository<Product> stockRepository)
		{
			this.stockRepository = stockRepository;
		}

		#endregion
		
		#region Methods

		public IEnumerable<Product> GetWaresByBrandId(int brandId)
		{
			return this.stockRepository.Where(x => x.Brand != null && x.Brand.Id == brandId);
		}
		
		public IEnumerable<Product> GetWaresByModelId(int modelId)
		{
			return this.stockRepository.Where(x => x.ModelId == modelId);
		}

		#endregion
	}
}