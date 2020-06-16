using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceCase.WebClient.ViewModels.Auth
{
	public class RegisterViewModel
	{
		#region Properties

		public bool IsHealthy { get; set; }
		
		[DataType(DataType.Text)]
		[DisplayName("Adı")]
		[Required(ErrorMessage="{0} alan zorunludur.")]
		public string FirstName { get; set; }

		[DataType(DataType.Text)]
		[DisplayName("Soyadı")]
		public string LastName { get; set; }

		[DataType(DataType.EmailAddress)]
		[DisplayName("Email")]
		[Required(ErrorMessage="{0} alan zorunludur.")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Şifre")]
		[Required(ErrorMessage="{0} alan zorunludur.")]    
		public string PasswordFirst { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Şifre Tekrar")]
		[Compare("PasswordFirst", ErrorMessage = "Şifreler uyuşmuyor.")]
		[Required(ErrorMessage="{0} alan zorunludur.")]
		public string PasswordSecond { get; set; }

		[Range(typeof(bool), "true", "true", ErrorMessage = "Üyelik sözleşmesini kabul etmeniz gerekmektedir.")]
		[Required(ErrorMessage="{0} alan zorunludur.")]
		[DisplayName("Üyelik Sözleşmesi")]
		public bool TermsAndConditions { get; set; }

		#endregion
	}
}