using ECommerceCase.Core.Models.Shopping;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.OMS.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShoppingCartsController : ControllerBase
	{
		#region Services

		private readonly IShoppingCartService shoppingCartService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="shoppingCartService"></param>
		public ShoppingCartsController(IShoppingCartService shoppingCartService)
		{
			this.shoppingCartService = shoppingCartService;
		}

		#endregion

		#region Methods

		[HttpGet]
		[Route("/shoppingcarts/{userId}")]
		public ActionResult<ShoppingCart> Get(string userId)
		{
			var shoppingCart = this.shoppingCartService.GetShoppingCart(userId);
			if (shoppingCart != null)
			{
				return shoppingCart;
			}
			else
			{
				return this.NotFound();
			}
		}
		
		[HttpPost]
		[Route("/shoppingcarts/{userId}/{productId}")]
		public ActionResult<ShoppingCart> Post(string userId, int productId)
		{
			var shoppingCart = this.shoppingCartService.AddToShoppingCart(userId, productId);
			if (shoppingCart != null)
			{
				return shoppingCart;
			}
			else
			{
				return this.NotFound();
			}
		}
		
		[HttpDelete]
		[Route("/shoppingcarts/{userId}/{productId}")]
		public ActionResult<ShoppingCart> Delete(string userId, int productId)
		{
			var shoppingCart = this.shoppingCartService.RemoveFromShoppingCart(userId, productId);
			if (shoppingCart != null)
			{
				return shoppingCart;
			}
			else
			{
				return this.NotFound();
			}
		}
		
		[HttpDelete]
		[Route("/shoppingcarts/{userId}")]
		public ActionResult<ShoppingCart> Delete(string userId)
		{
			var shoppingCart = this.shoppingCartService.EmptyShoppingCart(userId);
			if (shoppingCart != null)
			{
				return shoppingCart;
			}
			else
			{
				return this.NotFound();
			}
		}

		#endregion
	}
}