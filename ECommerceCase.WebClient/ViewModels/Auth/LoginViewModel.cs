using System.ComponentModel.DataAnnotations;

namespace ECommerceCase.WebClient.ViewModels.Auth
{
	public class LoginViewModel : ViewModelBase
	{
		#region Properties

		public bool IsHealthy { get; set; }
		
		[Required(ErrorMessage = "Bu alan zorunludur.")]
		public string Username { get; set; }


		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Bu alan zorunludur.")]
		public string Password { get; set; }
		
		public string ReturnUrlAfterLogin { get; set; }

		#endregion
	}
}