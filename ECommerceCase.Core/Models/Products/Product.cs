using System.Collections;
using System.Collections.Generic;
using ECommerceCase.Core.Models.Categorization;
using ECommerceCase.Core.Models.Monetary;

namespace ECommerceCase.Core.Models.Products
{
	public class Product : EntityBase
	{
		#region Properties

		public int ModelId { get; set; }
		
		public string Name { get; set; }
		
		public string Description { get; set; }
		
		public IEnumerable<string> Images { get; set; }
		
		public EmtiaCategory Category { get; set; }
		
		public ProductKind Kind { get; set; }
		
		public BrandModel Brand { get; set; }
		
		public PriceModel Price { get; set; }

		#endregion
	}
}