using System.Threading.Tasks;
using ECommerceCase.WebClient.Services.Interfaces;
using ErtisAuth.Core.Models.Auth;
using ErtisAuth.Core.Models.Users;
using ErtisAuth.Infrastructure;
using ErtisAuth.Services.Interfaces;

namespace ECommerceCase.WebClient.Services
{
	public class AuthService : IAuthService
	{
		#region Services

		private readonly IAuthenticationService ertisAuthService;
		private readonly IUserService ertisUserService;

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ertisAuthService"></param>
		/// <param name="ertisUserService"></param>
		public AuthService(IAuthenticationService ertisAuthService, IUserService ertisUserService)
		{
			this.ertisAuthService = ertisAuthService;
			this.ertisUserService = ertisUserService;
		}

		#endregion
		
		#region Methods

		public IResponseResult<AuthenticationToken> Login(string username, string password) => this.LoginAsync(username, password).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<AuthenticationToken>> LoginAsync(string username, string password)
		{
			return await this.ertisAuthService.GetTokenAsync(username, password);
		}

		public IResponseResult<User> Register(User model, string password) => this.RegisterAsync(model, password).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<User>> RegisterAsync(User model, string password)
		{
			return await this.ertisUserService.RegisterAsync(model, password);
		}

		public IResponseResult<AuthenticationToken> GetToken(string username, string password) => this.GetTokenAsync(username, password).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<AuthenticationToken>> GetTokenAsync(string username, string password)
		{
			return await this.ertisAuthService.GetTokenAsync(username, password);
		}
		
		public IResponseResult<Me> Me(string accessToken) => this.MeAsync(accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<Me>> MeAsync(string accessToken)
		{
			return await this.ertisAuthService.WhoAmIAsync(accessToken);
		}
		
		public IResponseResult<AuthenticationToken> RefreshToken(string refreshToken) => this.RefreshTokenAsync(refreshToken).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult<AuthenticationToken>> RefreshTokenAsync(string refreshToken)
		{
			return await this.ertisAuthService.RefreshTokenAsync(refreshToken);
		}
		
		public IResponseResult<AuthenticationToken> VerifyToken(string accessToken) => this.VerifyTokenAsync(accessToken).ConfigureAwait(false).GetAwaiter().GetResult();

		public async Task<IResponseResult<AuthenticationToken>> VerifyTokenAsync(string accessToken)
		{
			return await this.ertisAuthService.VerifyTokenAsync(accessToken);
		}
		
		public void RevokeToken(string token) => this.RevokeTokenAsync(token).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task RevokeTokenAsync(string token)
		{
			await this.ertisAuthService.RevokeTokenAsync(token);
		}
		
		public IResponseResult HealthCheck() => this.HealthCheckAsync().ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult> HealthCheckAsync()
		{
			return await this.ertisAuthService.HealthCheckAsync();
		}
		
		/*
		public SetPasswordResponseModel SetPassword(SetPasswordRequestModel model) => this.SetPasswordAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<SetPasswordResponseModel> SetPasswordAsync(SetPasswordRequestModel model)
		{
			var setPasswordResponse = await this.ertisAuthService.SetPasswordAsync(model.EmailAddress, model.NewPassword, model.ResetToken);
			if (setPasswordResponse.IsSuccess)
			{
				return new SetPasswordResponseModel
				{
					IsSuccessfull = true
				};
			}
			else
			{
				return new SetPasswordResponseModel
				{
					IsSuccessfull = false,
					Error = new Error
					{
						Message = setPasswordResponse.Message
					},
					ErrorText = setPasswordResponse.Message
				};
			}
		}
		
		public ResetPasswordResponseModel ResetPassword(ResetPasswordRequestModel model) => this.ResetPasswordAsync(model).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<ResetPasswordResponseModel> ResetPasswordAsync(ResetPasswordRequestModel model)
		{
			var resetPasswordResponse = await this.ertisAuthService.ResetPasswordAsync(model.Email);
			if (resetPasswordResponse.IsSuccess)
			{
				return new ResetPasswordResponseModel
				{
					IsSuccessfull = true,
					PasswordResetToken = resetPasswordResponse.Data.ResetToken,
					PasswordTokenExpireTime = resetPasswordResponse.Data.ExpireUnixTime
				};
			}
			else
			{
				return new ResetPasswordResponseModel
				{
					IsSuccessfull = false,
					Error = new Error
					{
						Message = resetPasswordResponse.Message
					},
					ErrorText = resetPasswordResponse.Message
				};
			}
		}
		
		public IResponseResult ChangePassword(ChangePasswordRequestModel model, string accessToken) => this.ChangePasswordAsync(model, accessToken).ConfigureAwait(false).GetAwaiter().GetResult();
		
		public async Task<IResponseResult> ChangePasswordAsync(ChangePasswordRequestModel model, string accessToken)
		{
			return await this.ertisAuthService.ChangePasswordAsync(model.UserId, model.NewPassword, accessToken);
		}
		*/

		#endregion
	}
}