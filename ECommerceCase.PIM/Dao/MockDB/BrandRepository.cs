using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Infrastructure.Dao.MockDB;

namespace ECommerceCase.PIM.Dao.MockDB
{
	public sealed class BrandRepository : RepositoryBase<BrandModel>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public BrandRepository()
		{
			this.Insert(new []
			{
				new BrandModel
				{
					Id = 100,
					Name = "BlueJeans"
				}
			});
		}

		#endregion
	}
}