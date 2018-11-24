using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
	public interface IEasyUserManager<TUser, TKey> where TUser : ApplicationUser<TKey>
	{
		Task<TUser> GetUserAsync(ClaimsPrincipal principal);
		Task<TKey> GetUserId(ClaimsPrincipal principal);
		Task<TKey> GetUserIdAsync(TUser user);
		Task<EasyIdentityResult> CreateAsync(TUser user, string password);
		Task<EasyIdentityResult> SetEmailAsync(TUser user, string email);
		Task<EasyIdentityResult> SetPhoneNumberAsync(TUser user, string number);
		Task<EasyIdentityResult> CreateAsync(TUser user);
		Task<string> GenerateSmsConfirmationTokenAsync(TUser user);
		//Task<EasyIdentityResult> AddLoginAsync(TUser user, UserLoginInfo login);
		Task<TUser> FindByIdAsync(string userId);
		Task<EasyIdentityResult> ConfirmPhoneNumberAsync(TUser user, string token);
		Task<TUser> FindByPhoneNumberAsync(string email);
		Task<bool> IsPhoneNumberConfirmedAsync(TUser user);
		Task<string> GeneratePasswordResetTokenAsync(TUser user);
		Task<EasyIdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword);
		Task<bool> HasPasswordAsync(TUser user);
		Task<EasyIdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword);
		Task<EasyIdentityResult> AddPasswordAsync(TUser user, string password);
		//Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user);


	}
}
