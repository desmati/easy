//using Microsoft.Extensions.Localization;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Microsoft.AspNetCore.Identity
//{
//    public class EasyRavenDbRoleStore<TRole> :
//        IRoleStore<TRole> where TRole : ApplicationRole
//    {
//        private IStringLocalizer _localizer;
//        public EasyRavenDbRoleStore(IStringLocalizer localizer)
//        {
//            _localizer = localizer;
//        }

//        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var currentRole = await DataStore.Roles.FindAsync(x => x.Name == role.Name);
//                if (currentRole?.Count > 0)
//                    return EasyIdentityResult.FailedIdentityResult(_localizer["نام رول تکراری است."], _localizer["RoleDuplicateErrorCode"]);
//                await DataStore.Roles.SaveAsync(role);
//                return IdentityResult.Success;
//            }
//            catch { }
//            return EasyIdentityResult.FailedIdentityResult(_localizer["متاسفانه خطایی رخ داد. لطفاً مجدداً تلاشش کنید."], _localizer["RoleCreateErrorCode"]);
//        }

//        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var usersInRole = await DataStore.Users.FindAsync(x => x.Roles.Contains(role.Name));
//                if (usersInRole?.Count > 0)
//                    return EasyIdentityResult.FailedIdentityResult(_localizer["کاربرانی هنوز در این رول هستند. ابتدا آن ها را پاک کنید."], _localizer["RoleHasUserErrorCode"]);
//                DataStore.Roles.Delete(role.Id);
//                return IdentityResult.Success;
//            }
//            catch { }
//            return EasyIdentityResult.FailedIdentityResult(_localizer["متاسفانه خطایی رخ داد. لطفاً مجدداً تلاش کنید."], _localizer["RoleDeleteErrorCode"]);
//        }

//        public void Dispose()
//        {
//        }

//        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
//        {
//            return await DataStore.Roles.LoadAsync<TRole>(roleId);
//        }

//        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
//        {
//            var roles = await DataStore.Roles.FindAsync<TRole>(x => x.NormalizedName == normalizedRoleName);
//            return roles?.FirstOrDefault();
//        }

//        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(role.Name.ToLower().Trim());
//        }

//        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(role.Id);
//        }

//        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
//        {
//            return Task.FromResult(role.Name);
//        }

//        public async Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
//        {
//            role.NormalizedName = normalizedName;
//            await UpdateAsync(role, cancellationToken);
//        }

//        public async Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
//        {
//            role.Name = roleName;
//            await UpdateAsync(role, cancellationToken);
//        }

//        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
//        {
//            await DataStore.Roles.SaveAsync(role);
//            return IdentityResult.Success;
//        }
//    }
//}
