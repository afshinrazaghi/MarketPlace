﻿using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IUserService : IAsyncDisposable
    {
        #region account
        public Task<RegisterUserResult> RegisterUser(RegisterUserDTO model);

        public Task<bool> IsUserExistsByMobileNumber(string mobileNumber);

        public Task<LoginUserResult> LoginUser(LoginUserDTO model);

        public Task<User?> GetUserByMobile(string mobileNumber);

        public Task<ForgotPasswordResult> RecoverPassword(ForgotPasswordDTO model);

        public Task<bool> ActivateMobile(ActivateMobileDTO model);
        #endregion
    }
}
