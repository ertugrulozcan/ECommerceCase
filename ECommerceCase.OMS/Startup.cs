using System;
using EasyCaching.Core.Configurations;
using ECommerceCase.Core.Models.Shopping;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.OMS.Services;
using ECommerceCase.OMS.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingCartRepository = ECommerceCase.OMS.Dao.MockDB.ShoppingCartRepository;

namespace ECommerceCase.OMS
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IRepository<ShoppingCart>, ShoppingCartRepository>();
			services.AddSingleton<IShoppingCartService, ShoppingCartService>();
			services.AddSingleton<IProductService, ProductService>();
			services.AddSingleton<IWarehouseService, WarehouseService>();

			try
			{
				string redisHost = this.Configuration["Redis.Host"];
				int redisPort = this.Configuration.GetValue<int>("Redis.Port");
				services.AddEasyCaching(options =>
				{
					options.UseRedis(redisConfiguration =>
						{
							redisConfiguration.DBConfig.Endpoints.Add(new ServerEndPoint(redisHost, redisPort));
							redisConfiguration.DBConfig.AllowAdmin = true;
						}, 
						"ShoppingCartsDistrubutedCacheChannel");
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}