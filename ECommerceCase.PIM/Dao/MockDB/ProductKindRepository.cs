using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Infrastructure.Dao.MockDB;

namespace ECommerceCase.PIM.Dao.MockDB
{
	public sealed class ProductKindRepository : RepositoryBase<ProductKind>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public ProductKindRepository()
		{
			this.Insert(new []
			{
				new ProductKind
				{
					Id = 100,
					Name = "Clothes"
				}
			});
		}

		#endregion
	}
}