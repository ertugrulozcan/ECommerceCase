using System.Collections.Generic;

namespace ECommerceCase.WebClient.Models.NavigationMenu
{
	public interface IMenuItem
	{
		string Title { get; }
		
		string Href { get; }
		
		List<IMenuItem> SubItems { get; }
	}
	
	public class MenuItem : IMenuItem
	{
		public string Title { get; set; }
		
		public string Href { get; set; }
		
		public List<IMenuItem> SubItems { get; set; }
	}
}