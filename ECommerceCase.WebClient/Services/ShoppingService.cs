using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Shopping;
using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Models.ShoppingCart;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Services
{
	public class ShoppingService : IShoppingService
	{
		#region Services

		private readonly ILogger<ShoppingService> _logger;

		#endregion
		
		#region Fields

		private HttpClient client;

		#endregion

		#region Properties

		private string OMS_API_URL { get; }
		
		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public ShoppingService(IConfiguration configuration, ILogger<ShoppingService> logger)
		{
			this._logger = logger;
			this.OMS_API_URL = configuration["OmsApiUrl"];
			
			Console.WriteLine($"ECommerseCase.WebClient/ShoppingService resolved. (OMS_API_URL: '{this.OMS_API_URL}')");
			
			this.client = new HttpClient();
		}

		#endregion
		
		#region Methods
		
		public IResponseResult<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(string userId) => GetShoppingCartItemsAsync(userId).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<IEnumerable<ShoppingCartItem>>> GetShoppingCartItemsAsync(string userId)
		{
			var response = await this.client.GetAsync($"{this.OMS_API_URL}/shoppingcarts/{userId}");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				var shoppingCart = Newtonsoft.Json.JsonConvert.DeserializeObject<ShoppingCart>(json);
				var groupedProducts = shoppingCart.CartItems.GroupBy(x => x.Id);
				
				List<ShoppingCartItem> items = new List<ShoppingCartItem>();
				foreach (var productGroup in groupedProducts)
				{
					items.Add(new ShoppingCartItem
					{
						Product = productGroup.FirstOrDefault(),
						Quantity = productGroup.Count()
					});
				}
				
				return new ResponseResult<IEnumerable<ShoppingCartItem>>(true) { Data = items };
			}
			else
			{
				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					return new ResponseResult<IEnumerable<ShoppingCartItem>>(true) { Data = new List<ShoppingCartItem>() };
				}
				else
				{
					this._logger.Log(LogLevel.Information, response.ReasonPhrase);
					return new ResponseResult<IEnumerable<ShoppingCartItem>>(false, response.ReasonPhrase);	
				}
			}
		}

		public IResponseResult AddToShoppingCart(string userId, in int productId) => AddToShoppingCartAsync(userId, productId).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult> AddToShoppingCartAsync(string userId, int productId)
		{
			var response = await this.client.PostAsync($"{this.OMS_API_URL}/shoppingcarts/{userId}/{productId}", new StringContent(string.Empty));
			if (response.IsSuccessStatusCode)
			{
				return new ResponseResult(true);
			}
			else
			{
				this._logger.Log(LogLevel.Information, response.ReasonPhrase);
				return new ResponseResult(false, response.ReasonPhrase);
			}
		}

		public IResponseResult RemoveFromShoppingCart(string userId, int productId) => RemoveFromShoppingCartAsync(userId, productId).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult> RemoveFromShoppingCartAsync(string userId, int productId)
		{
			var response = await this.client.DeleteAsync($"{this.OMS_API_URL}/shoppingcarts/{userId}/{productId}");
			if (response.IsSuccessStatusCode)
			{
				return new ResponseResult(true);
			}
			else
			{
				this._logger.Log(LogLevel.Information, response.ReasonPhrase);
				return new ResponseResult(false, response.ReasonPhrase);
			}
		}
		
		public IResponseResult EmptyShoppingCart(string userId) => EmptyShoppingCartAsync(userId).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult> EmptyShoppingCartAsync(string userId)
		{
			var response = await this.client.DeleteAsync($"{this.OMS_API_URL}/shoppingcarts/{userId}");
			if (response.IsSuccessStatusCode)
			{
				return new ResponseResult(true);
			}
			else
			{
				this._logger.Log(LogLevel.Information, response.ReasonPhrase);
				return new ResponseResult(false, response.ReasonPhrase);
			}
		}

		#endregion
	}
}