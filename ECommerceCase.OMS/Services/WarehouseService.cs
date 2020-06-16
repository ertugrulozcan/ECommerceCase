using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ECommerceCase.OMS.Services
{
	public class WarehouseService : IWarehouseService
	{
		#region Fields

		private HttpClient client;

		#endregion

		#region Properties

		private string WMS_API_URL { get; }
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public WarehouseService(IConfiguration configuration)
		{
			this.WMS_API_URL = configuration["WmsApiUrl"];
			Console.WriteLine($"ECommerseCase.OMS/WarehouseService resolved. (WMS_API_URL: '{this.WMS_API_URL}')");
			
			this.client = new HttpClient();
		}

		#endregion
		
		#region Methods

		public IResponseResult<IEnumerable<Product>> GetStockWares(int brandId) => GetStockWaresAsync(brandId).ConfigureAwait(false).GetAwaiter().GetResult();

		public async Task<IResponseResult<IEnumerable<Product>>> GetStockWaresAsync(int brandId)
		{
			var response = await this.client.GetAsync($"{this.WMS_API_URL}/stock/{brandId}");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				return new ResponseResult<IEnumerable<Product>>(true) { Data = result };
			}
			else
			{
				return new ResponseResult<IEnumerable<Product>>(false, response.ReasonPhrase);
			}
		}

		#endregion
	}
}