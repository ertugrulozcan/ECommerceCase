namespace ECommerceCase.WebClient.Models.Auth
{
	public class UserModel
	{
		#region Properties

		public string Id { get; set; }
		
		public string Username { get; set; }

		public string Email { get; set; }
        
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string FullName
		{
			get
			{
				return $"{this.FirstName} {this.LastName}";
			}
		}

		#endregion
	}
}