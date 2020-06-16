using System.Collections.Generic;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.PIM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.PIM.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		#region Services

		private readonly IProductsService productsService;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="productsService"></param>
		public ProductsController(IProductsService productsService)
		{
			this.productsService = productsService;
		}

		#endregion
		
		#region Methods

		[HttpGet]
		[Route("/products")]
		public IEnumerable<Product> Get()
		{
			return this.productsService.GetProducts();
		}
		
		[HttpGet]
		[Route("/products/{id}")]
		public Product Get(int id)
		{
			return this.productsService.GetProduct(id);
		}

		#endregion
	}
}