using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Infrastructure.Dao.MockDB;

namespace ECommerceCase.PIM.Dao.MockDB
{
	public sealed class CategoryRepository : RepositoryBase<EmtiaCategory>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public CategoryRepository()
		{
			this.Insert(new []
			{
				new EmtiaCategory
				{
					Id = 100,
					Name = "Shirts"
				},
				new EmtiaCategory
				{
					Id = 200,
					Name = "Dresses"
				},
				new EmtiaCategory
				{
					Id = 300,
					Name = "Jeans",
					SubCategories = new []
					{
						new EmtiaCategory
						{
							Id = 301,
							Name = "Skinny"
						},
						new EmtiaCategory
						{
							Id = 302,
							Name = "Relaxed"
						},
						new EmtiaCategory
						{
							Id = 303,
							Name = "Bootcut"
						},
						new EmtiaCategory
						{
							Id = 304,
							Name = "Straight"
						},
					}
				},
				new EmtiaCategory
				{
					Id = 400,
					Name = "Jackets"
				},
				new EmtiaCategory
				{
					Id = 500,
					Name = "Gymwear"
				},
				new EmtiaCategory
				{
					Id = 600,
					Name = "Blazers"
				},
				new EmtiaCategory
				{
					Id = 700,
					Name = "Shoes"
				}
			});
		}

		#endregion
	}
}