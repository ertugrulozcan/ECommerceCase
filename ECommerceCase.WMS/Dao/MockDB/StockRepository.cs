using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Products;
using ECommerceCase.Infrastructure.Dao.MockDB;

namespace ECommerceCase.WMS.Dao.MockDB
{
	public sealed class StockRepository : RepositoryBase<Product>
	{
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public StockRepository()
		{
			BrandModel mockBrand = new BrandModel
			{
				Id = 100,
				Name = "BlueJeans"
			};
			
			this.Insert(new []
			{
				new Product
				{
					Id = 10001,
					Name = "Ripped Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5001
				},
				new Product
				{
					Id = 10003,
					Name = "Washed Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5003
				},
				new Product
				{
					Id = 10004,
					Name = "Vintage Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5004
				},
				new Product
				{
					Id = 10006,
					Name = "Vintage Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5004
				},
				new Product
				{
					Id = 10007,
					Name = "Ripped Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5001
				},
				new Product
				{
					Id = 10008,
					Name = "Washed Skinny Jeans",
					Brand = mockBrand,
					ModelId = 5003
				},
			});
		}

		#endregion
	}
}