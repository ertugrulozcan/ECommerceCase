using System.Collections;
using System.Collections.Generic;
using ECommerceCase.WebClient.Models.NavigationMenu;

namespace ECommerceCase.WebClient.ViewModels.Menu
{
	public class NavigationMenuViewModel : ViewModelBase
	{
		#region Properties

		public IEnumerable<IMenuItem> Menu { get; set; }

		#endregion
	}
}