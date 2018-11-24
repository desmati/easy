//using System;
//using System.Data;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Microsoft.AspNetCore.Identity
//{
//	public class EasySignInManager<TUser, TKey> : IEasySignInManager<TUser, TKey>
//		where TUser : ApplicationUser<TKey>
//	{
//		private readonly IUserStore<TUser> _userStore;
//		private readonly IEasyStorage<EasySignInToken<Guid, TKey>, TKey> _tokensStore;
//		public EasySignInManager(IUserStore<TUser> userStore, IEasyStorage<EasySignInToken<Guid, TKey>, TKey> tokensStore)
//		{
//			_userStore = userStore;
//			_tokensStore = tokensStore;
//		}

//		public async Task<EasySignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure, CancellationToken cancellationToken)
//		{
//			var r = EasySignInResult.Success;
//			var user = await _userStore.FindByNameAsync(userName, cancellationToken);
//			if (user != null && !string.IsNullOrEmpty(password)
//				&& user?.PasswordHash == password.PasswordHash()
//				&& !user.LockoutEnabled && user.PhoneNumberConfirmed)
//			{
//				var token = new EasySignInToken<Guid, TKey>(user.Id);
//				if (isPersistent)
//				{
//					token.ExpiresAt = DateTime.Now.AddMonths(1);
//				}

//				//await _tokensStore.SaveAsync(token);
//				r.Token = token;
//				return r;
//			}
//			return EasySignInResult.Failed;
//		}

//		public async Task<EasySignInResult> SignInAsync(TUser user, bool isPersistent, CancellationToken cancellationToken, string authenticationMethod = null)
//		{
//			var r = EasySignInResult.Success;
//			var _user = await _userStore.FindByNameAsync(user.UserName, cancellationToken);
//			if (user != null
//				&& !user.LockoutEnabled && user.PhoneNumberConfirmed)
//			{
//				var token = new EasySignInToken(user.Id);
//				if (isPersistent)
//				{
//					token.ExpiresAt = DateTime.Now.AddMonths(1);
//				}

//				await DataStore.Tokens.SaveAsync(token);
//				r.Token = token;
//				return r;
//			}
//			return EasySignInResult.Failed;
//		}

//		public async Task SignOutAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
//		{
//			var _user = await _userStore.FindByNameAsync(user.Identity.Name, cancellationToken);
//			var tokens = await DataStore.Tokens.FindAsync(x => x.UserId == _user.Id && x.ExpiresAt > DateTime.Now);
//			await tokens.ForEachAsync(async token =>
//			 {
//				 token.ExpiresAt = DateTime.Now;
//				 await DataStore.Tokens.SaveAsync(token);
//			 });
//		}
//	}
//}
