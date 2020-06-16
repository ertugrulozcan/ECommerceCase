using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Rest;

namespace ECommerceCase.WebClient.Services.Interfaces
{
	public interface ICategoryService
	{
		IResponseResult<IEnumerable<EmtiaCategory>> GetCategories();

		Task<IResponseResult<IEnumerable<EmtiaCategory>>> GetCategoriesAsync();
	}
}