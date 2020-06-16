using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;

namespace ECommerceCase.PIM.Services.Interfaces
{
	public interface ICategoryService
	{
		IEnumerable<EmtiaCategory> GetCategories();
		
		EmtiaCategory GetCategoryById(int id);
		
		ProductKind GetProductKindById(int id);
	}
}