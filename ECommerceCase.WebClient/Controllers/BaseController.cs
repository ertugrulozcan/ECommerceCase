using System;
using System.Linq;
using ECommerceCase.WebClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.WebClient.Controllers
{
	public abstract class BaseController : Controller
	{
		#region Services

		private readonly IAuthService authService;

		#endregion
		
		#region Properties

		protected string UserId => this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

		protected string AccessToken => this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value;

		protected bool IsLoggedIn
		{
			get
			{
				try
				{
					return this.authService.VerifyToken(this.AccessToken).IsSuccess;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return false;
				}
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="authService"></param>
		protected BaseController(IAuthService authService)
		{
			this.authService = authService;
		}

		#endregion
	}
}