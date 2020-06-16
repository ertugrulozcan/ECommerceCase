using System.Collections.Generic;

namespace ECommerceCase.WebClient.ViewModels.Products
{
	public class ProductsGridViewModel : ViewModelBase
	{
		#region Properties

		public IList<ProductCardViewModel> Cards { get; set; }

		#endregion
	}
}