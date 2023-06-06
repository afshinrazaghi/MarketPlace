using MarketPlace.DataLayer.DTOs.Account;
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
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO model);

        Task<bool> IsUserExistsByMobileNumber(string mobileNumber);

        Task<LoginUserResult> LoginUser(LoginUserDTO model);

        Task<User?> GetUserByMobile(string mobileNumber);

        Task<ForgotPasswordResult> RecoverPassword(ForgotPasswordDTO model);

        Task<bool> ActivateMobile(ActivateMobileDTO model);

        Task<ChangePasswordResult> ChangeUserPassword(ChangePasswordDTO model, long currentUserId);

        Task<EditProfileDTO?> GetProfileForEdit(long userId);
        #endregion
    }
}
