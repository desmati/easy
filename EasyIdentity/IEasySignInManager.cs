//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Microsoft.AspNetCore.Identity
//{
//	public interface IEasySignInManager<TUser, TKey> where TUser : ApplicationUser<TKey>
//	{
//		Task<EasySignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure, CancellationToken cancellationToken);
//		Task<EasySignInResult> SignInAsync(TUser user, bool isPersistent, CancellationToken cancellationToken, string authenticationMethod = null);
//		Task SignOutAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
//	}
//}
