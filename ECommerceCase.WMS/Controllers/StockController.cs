using System.Collections.Generic;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.WMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.WMS.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StockController : ControllerBase
	{
		#region Services

		private readonly IStockService stockService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="stockService"></param>
		public StockController(IStockService stockService)
		{
			this.stockService = stockService;
		}

		#endregion

		#region Methods

		[HttpGet]
		[Route("/stock/{brandId}")]
		public IEnumerable<Product> Get(int brandId)
		{
			return this.stockService.GetWaresByBrandId(brandId);
		}

		#endregion
	}
}