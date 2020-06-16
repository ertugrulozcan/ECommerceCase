using System.Threading.Tasks;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Infrastructure;

namespace ECommerceCase.WebClient.Services.Interfaces
{
	public interface IAuthService
	{
		IResponseResult<AuthenticationToken> Login(string username, string password);
		Task<IResponseResult<AuthenticationToken>> LoginAsync(string username, string password);

		IResponseResult<User> Register(User model, string password);
		Task<IResponseResult<User>> RegisterAsync(User model, string password);

		IResponseResult<AuthenticationToken> GetToken(string username, string password);
		Task<IResponseResult<AuthenticationToken>> GetTokenAsync(string username, string password);
		
		IResponseResult<AuthenticationToken> RefreshToken(string refreshToken);
		Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(string refreshToken);
		
		IResponseResult<AuthenticationToken> VerifyToken(string accessToken);
		Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(string accessToken);

		IResponseResult<Me> Me(string accessToken);
		Task<IResponseResult<Me>> MeAsync(string accessToken);
		
		/*
		ResetPasswordResponseModel ResetPassword(ResetPasswordRequestModel model);
		Task<ResetPasswordResponseModel> ResetPasswordAsync(ResetPasswordRequestModel model);

		SetPasswordResponseModel SetPassword(SetPasswordRequestModel model);
		Task<SetPasswordResponseModel> SetPasswordAsync(SetPasswordRequestModel model);

		IResponseResult ChangePassword(ChangePasswordRequestModel model, string accessToken);

		Task<IResponseResult> ChangePasswordAsync(ChangePasswordRequestModel model, string accessToken);
		*/

		void RevokeToken(string token);

		Task RevokeTokenAsync(string token);

		IResponseResult HealthCheck();

		Task<IResponseResult> HealthCheckAsync();
	}
}