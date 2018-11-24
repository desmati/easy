//using Microsoft.Extensions.Localization;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Microsoft.AspNetCore.Identity
//{
//    public class EasyRavenDbUserStore<TUser> :
//        IUserStore<TUser> where TUser : ApplicationUser
//    {
//        private IStringLocalizer _localizer;
//        public EasyRavenDbUserStore(IStringLocalizer localizer)
//        {
//            _localizer = localizer;
//        }

//        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var currentUser = await DataStore.Users.FindAsync(x => x.PhoneNumber == user.PhoneNumber);
//                if (currentUser?.Count > 0)
//                    return EasyIdentityResult.FailedIdentityResult(_localizer["کابری با این شماره موبایل موجود است."], _localizer["UserDuplicateErrorCode"]);
//                user.NormalizedUserName = await GetNormalizedUserNameAsync(user, cancellationToken);
//                await DataStore.Users.SaveAsync(user);
//                return IdentityResult.Success;
//            }
//            catch { }
//            return EasyIdentityResult.FailedIdentityResult(_localizer["متاسفانه خطایی رخ داد. لطفاً مجدداً تلاشش کنید."], _localizer["UserCreateErrorCode"]);
//        }

//        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var currentUser = await DataStore.Users.FindAsync(x => x.PhoneNumber == user.PhoneNumber);
//                if (currentUser?.Count <= 0)
//                    return EasyIdentityResult.FailedIdentityResult(_localizer["کابری با این مشخصات یافت نشد."], _localizer["UserNotFoundErrorCode"]);
//                DataStore.Users.Delete(user.Id);
//                return IdentityResult.Success;
//            }
//            catch { }
//            return EasyIdentityResult.FailedIdentityResult(_localizer["متاسفانه خطایی رخ داد. لطفاً مجدداً تلاشش کنید."], _localizer["UserCreateErrorCode"]);
//        }

//        public void Dispose()
//        {

//        }

//        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
//        {
//            return await DataStore.Users.LoadAsync<TUser>(userId);
//        }

//        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
//        {
//            var list = await DataStore.Users.FindAsync<TUser>(x => x.NormalizedUserName == normalizedUserName);
//            return list?.FirstOrDefault();
//        }

//        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(user.UserName.ToLower().Trim());
//        }

//        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(user.Id);
//        }

//        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(user.UserName);
//        }

//        public async Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
//        {
//            user.NormalizedUserName = normalizedName;
//            await UpdateAsync(user, cancellationToken);
//        }

//        public async Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
//        {
//            user.UserName = userName;
//            await UpdateAsync(user, cancellationToken);
//        }

//        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
//        {
//            user.NormalizedUserName = await GetNormalizedUserNameAsync(user, cancellationToken);
//            await DataStore.Users.SaveAsync(user);
//            return IdentityResult.Success;
//        }
//    }
//}
