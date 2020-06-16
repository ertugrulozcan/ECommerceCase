using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao;
using ECommerceCase.PIM.Dao.MockDB;
using ECommerceCase.PIM.Services;
using ECommerceCase.PIM.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerceCase.PIM
{
	public class Startup
	{
		#region Services

		public IConfiguration Configuration { get; }

		#endregion
		
		#region Constructors

		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		#endregion
		
		#region Methods

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IRepository<Product>, ProductsRepository>();
			services.AddSingleton<IRepository<EmtiaCategory>, CategoryRepository>();
			services.AddSingleton<IRepository<ProductKind>, ProductKindRepository>();
			services.AddSingleton<IRepository<BrandModel>, BrandRepository>();
			
			services.AddSingleton<ICategoryService, CategoryService>();
			services.AddSingleton<IProductsService, ProductsService>();
			
			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);
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

		#endregion
	}
}