using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.PIM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceCase.PIM.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		#region Services

		private readonly ICategoryService categoryService;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="categoryService"></param>
		public CategoryController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		#endregion
		
		#region Methods

		[HttpGet]
		[Route("/categories")]
		public IEnumerable<EmtiaCategory> Get()
		{
			return this.categoryService.GetCategories();
		}
		
		[HttpGet]
		[Route("/categories/{id}")]
		public EmtiaCategory Get(int id)
		{
			return this.categoryService.GetCategoryById(id);
		}

		#endregion
	}
}