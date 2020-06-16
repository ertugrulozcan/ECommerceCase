using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceCase.WebClient.Extensions;
using ECommerceCase.WebClient.Services.Interfaces;
using ECommerceCase.WebClient.ViewModels.Auth;
using ErtisAuth.Core.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ECommerceCase.WebClient.Controllers
{
	public class AuthController : BaseController
	{
		#region Services

		private readonly IAuthService authService;
		private readonly ILogger<AuthController> _logger;
		
		#endregion

		#region Properties

		private string AdministratorToken { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="authService"></param>
		/// <param name="configuration"></param>
		/// <param name="logger"></param>
		public AuthController(IAuthService authService, IConfiguration configuration, ILogger<AuthController> logger) : base(authService)
		{
			this.authService = authService;
			this._logger = logger;

			this.AdministratorToken = configuration.GetSection("ErtisAuth")["AdministratorToken"];
		}

		#endregion

		#region Login & Logout

		[Route("login")]
        public IActionResult Login()
		{
			if (this.IsLoggedIn)
			{
				return this.RedirectToAction("Index", "Home");
			}
			
			var referer = this.Request.GetReferer();
			if (referer != null)
			{
				this.ViewBag.ReturnUrl = referer;
			}

			LoginViewModel viewModel = new LoginViewModel
			{
				IsHealthy = this.authService.HealthCheck().IsSuccess
			};
			
            return this.View(viewModel);
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
			if (this.ModelState.IsValid)
            {
                var getTokenResult = await this.authService.LoginAsync(model.Username, model.Password);
				if (getTokenResult.IsSuccess)
				{
					// Who am i?
					var meResult = await this.authService.MeAsync(getTokenResult.Data.AccessToken);
					if (meResult.IsSuccess)
					{
						var claims = new List<Claim>
						{
							new Claim("id", meResult.Data.Id),
							new Claim("username", meResult.Data.Username ?? string.Empty),
							new Claim("email", meResult.Data.EmailAddress ?? string.Empty),
							new Claim("firstName", meResult.Data.FirstName ?? string.Empty),
							new Claim("lastName", meResult.Data.LastName ?? string.Empty),
							new Claim("role", meResult.Data.Role ?? string.Empty),
							new Claim("access_token", getTokenResult.Data.AccessToken),
							new Claim("refresh_token", getTokenResult.Data.RefreshToken)
						};

						var authProperties = new AuthenticationProperties
						{
							IsPersistent = true,
							AllowRefresh = true
						};

						var authTokens = new List<AuthenticationToken>
						{
							new AuthenticationToken
							{
								Name = "AccessToken",
								Value = getTokenResult.Data.AccessToken
							},
							new AuthenticationToken
							{
								Name = "RefreshToken",
								Value = getTokenResult.Data.RefreshToken
							}
						};

						authProperties.StoreTokens(authTokens);

						await this.HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "id", "role")), authProperties);

						if (model.ReturnUrlAfterLogin != null && new Uri(model.ReturnUrlAfterLogin).IsValidUrl())
						{
							return this.Redirect(model.ReturnUrlAfterLogin);	
						}
						else
						{
							return this.RedirectToAction("Index", "Home");	
						}
					}
					else
					{
						_logger.Log(LogLevel.Information, meResult.Message);
						this.ModelState.AddModelError(string.Empty, meResult.Message);
						return this.View(model);
					}
				}
                else
                {
					_logger.Log(LogLevel.Information, getTokenResult.Message);
					this.ModelState.AddModelError(string.Empty, getTokenResult.Message);
					return this.View(model);
                }
            }

            return this.View(model);
		}

		[Route("logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
			var accessTokenClaim = this.User.Claims.FirstOrDefault(x => x.Type == "access_token");
			if (accessTokenClaim != null)
			{
				await this.authService.RevokeTokenAsync(accessTokenClaim.Value);	
			}
			
			var refreshTokenClaim = this.User.Claims.FirstOrDefault(x => x.Type == "refresh_token");
			if (refreshTokenClaim != null)
			{
				await this.authService.RevokeTokenAsync(refreshTokenClaim.Value);	
			}
			
			await this.HttpContext.SignOutAsync();
			if (string.IsNullOrEmpty(returnUrl))
				return this.Redirect("/");
			else
				return this.Redirect(returnUrl);
        }

		#endregion
		
		#region Register

		[Route("register")]
        public IActionResult Register()
        {
			if (this.IsLoggedIn)
			{
				return this.RedirectToAction("Index", "Home");
			}
			
            return this.View();
        }
		
        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
		{
			string firstName;
			string lastName;
			
			var fullNameWords = model.FirstName.Split(" ");
			if (fullNameWords.Length > 1)
			{
				firstName = string.Join(" ", fullNameWords.Take(fullNameWords.Length - 1));
				lastName = fullNameWords.LastOrDefault().Trim();
			}
			else
			{
				firstName = model.FirstName;
				lastName = string.Empty;
			}
			
			if (this.ModelState.IsValid)
            {
				if (model.PasswordFirst == null || model.PasswordSecond == null || model.PasswordFirst.Trim() != model.PasswordSecond.Trim())
				{
					this.ModelState.AddModelError(string.Empty, "Passwords does not matched!");
					return this.View(model);
				}
				
                var registerResponse = await this.authService.RegisterAsync(new User
				{
					Username = model.Email,
					EmailAddress = model.Email,
					FirstName = firstName,
					LastName = lastName,
					Role = "enduser",
					Status = "active"
				}, model.PasswordFirst);
				
                if (registerResponse.IsSuccess)
				{
					return await this.Login(new LoginViewModel { Username = model.Email, Password = model.PasswordFirst });
                }
                else
                {
					this._logger.Log(LogLevel.Error, registerResponse.Message);
					this.ModelState.AddModelError(string.Empty, registerResponse.Message);
					return this.View(model);
                }
            }
            else
            {
                return this.View(model);
            }
		}

		#endregion
	}
}