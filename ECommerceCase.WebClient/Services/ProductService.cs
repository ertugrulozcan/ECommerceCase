using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Services
{
	public class ProductService : IProductService
	{
		#region Services

		private readonly ILogger<ProductService> _logger;

		#endregion
		
		#region Fields

		private HttpClient client;

		#endregion

		#region Properties

		private string PIM_API_URL { get; }
		
		private string WMS_API_URL { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public ProductService(IConfiguration configuration, ILogger<ProductService> logger)
		{
			this._logger = logger;
			this.PIM_API_URL = configuration["PimApiUrl"];
			this.WMS_API_URL = configuration["WmsApiUrl"];
			
			Console.WriteLine($"ECommerseCase.WebClient/ProductService resolved. (PIM_API_URL: '{this.PIM_API_URL}')");
			Console.WriteLine($"ECommerseCase.WebClient/ProductService resolved. (WMS_API_URL: '{this.WMS_API_URL}')");
			
			this.client = new HttpClient();
		}

		#endregion
		
		#region Methods

		public IResponseResult<IEnumerable<Product>> GetProducts() => GetProductsAsync().ConfigureAwait(false).GetAwaiter().GetResult();

		public async Task<IResponseResult<IEnumerable<Product>>> GetProductsAsync()
		{
			var response = await this.client.GetAsync($"{this.PIM_API_URL}/products");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
				return new ResponseResult<IEnumerable<Product>>(true) { Data = result };
			}
			else
			{
				this._logger.Log(LogLevel.Information, response.ReasonPhrase);
				return new ResponseResult<IEnumerable<Product>>(false, response.ReasonPhrase);
			}
		}
		
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
				this._logger.Log(LogLevel.Information, response.ReasonPhrase);
				return new ResponseResult<IEnumerable<Product>>(false, response.ReasonPhrase);
			}
		}

		#endregion
	}
}