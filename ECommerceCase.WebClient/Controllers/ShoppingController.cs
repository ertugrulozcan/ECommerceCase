using ECommerceCase.Core.Rest;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Controllers
{
	public class ShoppingController : BaseController
	{
		#region Services

		private readonly ILogger<ShoppingController> _logger;
		private readonly IShoppingService shoppingService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="authService"></param>
		/// <param name="shoppingService"></param>
		/// <param name="logger"></param>
		public ShoppingController(IAuthService authService, IShoppingService shoppingService, ILogger<ShoppingController> logger) : base(authService)
		{
			this.shoppingService = shoppingService;
			this._logger = logger;
		}

		#endregion
		
		#region Index

		public IActionResult AddToShoppingCart(int productId)
		{
			if (!this.IsLoggedIn)
			{
				return this.Unauthorized();
			}

			IResponseResult response = this.shoppingService.AddToShoppingCart(this.UserId, productId);
			if (response.IsSuccess)
			{
				return this.RedirectToAction("Index", "Home");
			}
			else
			{
				this._logger.Log(LogLevel.Error, response.Message);
				return this.Problem();
			}
		}
		
		public IActionResult RemoveFromShoppingCart(int productId)
		{
			if (!this.IsLoggedIn)
			{
				return this.Unauthorized();
			}

			IResponseResult response = this.shoppingService.RemoveFromShoppingCart(this.UserId, productId);
			if (response.IsSuccess)
			{
				return this.RedirectToAction("Index", "Home");
			}
			else
			{
				this._logger.Log(LogLevel.Error, response.Message);
				return this.Problem();
			}
		}
		
		public IActionResult EmptyShoppingCart()
		{
			if (!this.IsLoggedIn)
			{
				return this.Unauthorized();
			}

			IResponseResult response = this.shoppingService.EmptyShoppingCart(this.UserId);
			if (response.IsSuccess)
			{
				return this.RedirectToAction("Index", "Home");
			}
			else
			{
				this._logger.Log(LogLevel.Error, response.Message);
				return this.Problem();
			}
		}
		
		#endregion
	}
}