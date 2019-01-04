using Microsoft.Extensions.Localization;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public class EasyUserManager<TUser, TKey> : IEasyUserManager<TUser, TKey> where TUser : ApplicationUser<TKey>
    {
        private IStringLocalizer localizer { get; set; }
        private IUserStore<TUser> userStore { get; set; }
        public EasyUserManager(IStringLocalizer localizer, IUserStore<TUser> userStore)
        {
            this.localizer = localizer;
            this.userStore = userStore;
        }

        //public Task<EasyIdentityResult> AddLoginAsync(TUser user, UserLoginInfo login)
        //{
        //    throw  new NotImplementedException();
        //}

        public async Task<EasyIdentityResult> AddPasswordAsync(TUser user, string password)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            _user.PasswordHash = password.PasswordHash();
            await userStore.UpdateAsync(_user, new CancellationToken());
            return EasyIdentityResult.Success;
        }

        public async Task<EasyIdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            if (_user != null && !string.IsNullOrEmpty(currentPassword) && _user.PasswordHash == currentPassword.PasswordHash())
            {
                _user.PasswordHash = newPassword.PasswordHash();
                await userStore.UpdateAsync(_user, new CancellationToken());
                return EasyIdentityResult.Success;
            }
            return EasyIdentityResult.Failed(localizer["UserManagerChangePasswordError"], localizer["خطایی رخ داد. لطفاً مجدداً تلاش کنید."]);
        }

        public async Task<EasyIdentityResult> ConfirmPhoneNumberAsync(TUser user, string token)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            if (_user != null && !string.IsNullOrEmpty(token))
            {
                if (_user.ConfirmPhoneNumberToken != token)
                {
                    return EasyIdentityResult.Failed(localizer["UserManagerConfirmPhoneNumberError"], localizer["کد وارد شده اشتباه است."]);
                }

                if (_user.ConfirmPhoneNumberTokenExpiresAt <= DateTime.Now)
                {
                    return EasyIdentityResult.Failed(localizer["UserManagerConfirmPhoneNumberError"], localizer["کد وارد شده منقضی شده است. لطفاً کد جدید دریافت کنید."]);
                }

                _user.PhoneNumberConfirmed = true;
                await userStore.UpdateAsync(_user, new CancellationToken());
                return EasyIdentityResult.Success;
            }
            else
            {
                return EasyIdentityResult.Failed(localizer["UserManagerConfirmPhoneNumberError"], localizer["کاربری با این مشخصات یافت نشد."]);
            }
        }

        public async Task<EasyIdentityResult> CreateAsync(TUser user, string password)
        {
            user.PasswordHash = password.PasswordHash();
            await userStore.UpdateAsync(user, new CancellationToken());
            return EasyIdentityResult.Success;
        }

        public async Task<EasyIdentityResult> CreateAsync(TUser user)
        {
            var r = await userStore.CreateAsync(user, new CancellationToken());
            return r.MapTo<EasyIdentityResult>();
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            return await userStore.FindByIdAsync(userId, new CancellationToken());
        }

        public async Task<string> GeneratePasswordResetTokenAsync(TUser user)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            _user.ConfirmPasswordToken = EasyString.RandomNumericalString();
            _user.ConfirmPasswordTokenExpiresAt = DateTime.Now.AddMinutes(2);
            await userStore.UpdateAsync(_user, new CancellationToken());
            return _user.ConfirmPasswordToken;
        }

        public async Task<string> GenerateSmsConfirmationTokenAsync(TUser user)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            _user.ConfirmPhoneNumberToken = EasyString.RandomNumericalString();
            _user.ConfirmPhoneNumberTokenExpiresAt = DateTime.Now.AddMinutes(2);
            await userStore.UpdateAsync(_user, new CancellationToken());
            return _user.ConfirmPhoneNumberToken;
        }

        public async Task<TUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await userStore.FindByNameAsync(principal.Identity.Name, new CancellationToken());
        }

        public async Task<TKey> GetUserId(ClaimsPrincipal principal)
        {
            var user = await userStore.FindByNameAsync(principal.Identity.Name, new CancellationToken());
            return user.Id;
        }

        public async Task<TKey> GetUserIdAsync(TUser user)
        {
            return await Task.Run(() =>
            {
                return user.Id;
            });
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<bool> IsPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public async Task<EasyIdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            if (_user != null && !string.IsNullOrEmpty(token))
            {
                if (_user.ConfirmPasswordToken != token)
                {
                    return EasyIdentityResult.Failed(localizer["UserManagerResetPasswordError"], localizer["کد وارد شده اشتباه است."]);
                }

                if (_user.ConfirmPasswordTokenExpiresAt <= DateTime.Now)
                {
                    return EasyIdentityResult.Failed(localizer["UserManagerResetPasswordError"], localizer["کد وارد شده منقضی شده است. لطفاً کد جدید دریافت کنید."]);
                }

                _user.PasswordHash = newPassword.PasswordHash();
                await userStore.UpdateAsync(_user, new CancellationToken());
                return EasyIdentityResult.Success;
            }
            else
            {
                return EasyIdentityResult.Failed(localizer["UserManagerResetPasswordError"], localizer["کاربری با این مشخصات یافت نشد."]);
            }
        }

        public async Task<EasyIdentityResult> SetEmailAsync(TUser user, string email)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            _user.Email = email;
            _user.NormalizedEmail = email.ToLower().Trim();
            await userStore.UpdateAsync(_user, new CancellationToken());
            return EasyIdentityResult.Success;
        }

        public async Task<EasyIdentityResult> SetPhoneNumberAsync(TUser user, string number)
        {
            var _user = await userStore.FindByNameAsync(user.UserName, new CancellationToken());
            _user.PhoneNumber = number;
            await userStore.UpdateAsync(_user, new CancellationToken());
            return EasyIdentityResult.Success;
        }

        public Task<TUser> FindByPhoneNumberAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
