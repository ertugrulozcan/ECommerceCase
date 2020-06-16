using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Services
{
	public class CategoryService : ICategoryService
	{
		#region Services

		private readonly ILogger<CategoryService> _logger;

		#endregion
		
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
		/// <param name="logger"></param>
		public CategoryService(IConfiguration configuration, ILogger<CategoryService> logger)
		{
			this._logger = logger;
			this.PIM_API_URL = configuration["PimApiUrl"];
			this.client = new HttpClient();
			
			Console.WriteLine($"ECommerseCase.WebClient/CategoryService resolved. (PIM_API_URL: '{this.PIM_API_URL}')");
		}

		#endregion
		
		#region Methods

		public IResponseResult<IEnumerable<EmtiaCategory>> GetCategories() => GetCategoriesAsync().ConfigureAwait(false).GetAwaiter().GetResult();

		public async Task<IResponseResult<IEnumerable<EmtiaCategory>>> GetCategoriesAsync()
		{
			var response = await this.client.GetAsync($"{this.PIM_API_URL}/categories");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EmtiaCategory>>(json);
				return new ResponseResult<IEnumerable<EmtiaCategory>>(true) { Data = result };
			}
			else
			{
				this._logger.Log(LogLevel.Critical, response.ReasonPhrase);
				return new ResponseResult<IEnumerable<EmtiaCategory>>(false, response.ReasonPhrase);
			}
		}

		#endregion
	}
}