using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs;
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
		private readonly IPasswordHelper _passwordHelper;
		#region Constructor
		private readonly IGenericRepository<User> _userRepository;

		public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
		{
			_userRepository = userRepository;
			_passwordHelper = passwordHelper;
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
					Password = _passwordHelper.HashPassword(model.Password)
				};

				await _userRepository.AddEntity(user);
				await _userRepository.SaveChanges();
				return RegisterUserResult.Success;
			}

			return RegisterUserResult.MobileExists;
		}

		public async Task<bool> IsUserExistsByMobileNumber(string mobileNumber)
		{
			return await _userRepository.GetQuery().AnyAsync(u => u.Mobile == mobileNumber);
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
