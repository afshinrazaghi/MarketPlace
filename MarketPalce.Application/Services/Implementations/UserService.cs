using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly IPasswordHelper _passwordHelper;
        private readonly IGenericRepository<User> _userRepository;
        private readonly ISmsService _smsService;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, ISmsService smsService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _smsService = smsService;
        }
        #endregion

        #region account
        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO model)
        {
            if (!await IsUserExistsByMobileNumber(model.Mobile))
            {
                var user = new User
                {
                    Mobile = model.Mobile,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = _passwordHelper.HashPassword(model.Password),
                    MobileActiveCode = new Random().Next(10000, 99999).ToString(),
                    EmailActiveCode = Guid.NewGuid().ToString("N")
                };

                await _userRepository.AddEntity(user);
                _smsService.SendSms(user.Mobile, user.MobileActiveCode);
                await _userRepository.SaveChanges();
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }

        public async Task<bool> IsUserExistsByMobileNumber(string mobileNumber)
        {
            return await _userRepository.GetQuery().AnyAsync(u => u.Mobile == mobileNumber);
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO model)
        {
            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(u => u.Mobile == model.Mobile);
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActivated;
            if (!_passwordHelper.VerifyPassword(model.Password, user.Password)) return LoginUserResult.NotFound;
            return LoginUserResult.Success;
        }

        public async Task<User?> GetUserByMobile(string mobileNumber)
        {
            return await _userRepository.GetQuery().SingleOrDefaultAsync(u => u.Mobile == mobileNumber);
        }

        public async Task<ForgotPasswordResult> RecoverPassword(ForgotPasswordDTO model)
        {
            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(u => u.Mobile == model.Mobile);
            if (user == null) return ForgotPasswordResult.NotFound;

            var newPassword = new Random().Next(1000000, 9999999).ToString();
            user.Password = _passwordHelper.HashPassword(newPassword);
            _userRepository.EditEntity(user);
            _smsService.SendUserPasswordSms(user.Mobile, newPassword);
            await _userRepository.SaveChanges();
            return ForgotPasswordResult.Success;
        }

        public async Task<bool> ActivateMobile(ActivateMobileDTO model)
        {
            var user = await _userRepository.GetQuery().SingleOrDefaultAsync(u => u.Mobile == model.Mobile);
            if (user is not null)
            {
                if (user.MobileActiveCode == model.MobileActiveCode)
                {
                    user.IsMobileActive = true;
                    user.MobileActiveCode = new Random().Next(10000, 99999).ToString();
                    _userRepository.EditEntity(user);
                    await _userRepository.SaveChanges();
                    return true;
                }

            }

            return false;
        }

        public async Task<ChangePasswordResult> ChangeUserPassword(ChangePasswordDTO model, long currentUserId)
        {
            var user = await _userRepository.GetEntityById(currentUserId);
            if (user is null) return ChangePasswordResult.UserNotFound;
            if (!_passwordHelper.VerifyPassword(model.CurrentPassword, user.Password)) return ChangePasswordResult.CurrentPasswordIncorrect;
            if (_passwordHelper.VerifyPassword(model.NewPassword, user.Password)) return ChangePasswordResult.UseNewPassword;
            user.Password = _passwordHelper.HashPassword(model.NewPassword);
            _userRepository.EditEntity(user);
            await _userRepository.SaveChanges();
            return ChangePasswordResult.Success;
        }


        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }




        #endregion
    }
}
