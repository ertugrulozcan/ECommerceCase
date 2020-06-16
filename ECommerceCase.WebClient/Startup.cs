using System;
using ECommerceCase.WebClient.Services;
using ECommerceCase.WebClient.Services.Interfaces;
using ErtisAuth.Boot;
using ErtisAuth.Extensions.AspNetCore;
using ErtisAuth.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceCase.WebClient
{
	public class Startup
	{
		#region Constants

		public const string CookieScheme = "auth";

		#endregion
		
		#region Properties

		public IConfiguration Configuration { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		#endregion
		
		#region Methods

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieScheme) // Sets the default scheme to cookies
				.AddCookie(CookieScheme, options =>
				{
					options.LoginPath = "/login";
					options.LogoutPath = "/logout";
				});

			// Initializing ErtisAuth.Net
			Bootstrapper bootstrapper = new Bootstrapper(this.Configuration.GetErtisAuthConfiguration());
			bootstrapper.InitializeServices(services);
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			IAuthenticationService ertisAuthService = serviceProvider.GetService<IAuthenticationService>();
			IUserService ertisUserService = serviceProvider.GetService<IUserService>();
			services.AddScoped<IAuthService>(s => new AuthService(ertisAuthService, ertisUserService));
			
			services.AddSingleton<IProductService, ProductService>();
			services.AddSingleton<ICategoryService, CategoryService>();
			services.AddSingleton<IShoppingService, ShoppingService>();
			services.AddSingleton<IMenuService, MenuService>();
			
			services
				.AddControllersWithViews()
				.AddRazorRuntimeCompilation();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();
			app.UseAuthentication();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		#endregion
	}
}