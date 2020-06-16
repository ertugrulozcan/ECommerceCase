using System;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Core.Rest;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ECommerceCase.OMS.Services
{
	public class ProductService : IProductService
	{
		#region Fields

		private HttpClient client;

		#endregion

		#region Properties

		private string PIM_API_URL { get; }
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public ProductService(IConfiguration configuration)
		{
			this.PIM_API_URL = configuration["PimApiUrl"];
			Console.WriteLine($"ECommerseCase.OMS/ProductService resolved. (PIM_API_URL: '{this.PIM_API_URL}')");
			
			this.client = new HttpClient();
		}

		#endregion
		
		#region Methods

		public IResponseResult<Product> GetProductById(int productId) => GetProductByIdAsync(productId).ConfigureAwait(false).GetAwaiter().GetResult();

		public async Task<IResponseResult<Product>> GetProductByIdAsync(int productId)
		{
			var response = await this.client.GetAsync($"{this.PIM_API_URL}/products/{productId}");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(json);
				return new ResponseResult<Product>(true) { Data = result };
			}
			else
			{
				return new ResponseResult<Product>(false, response.ReasonPhrase);
			}
		}

		#endregion
	}
}