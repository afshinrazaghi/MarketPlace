using MarketPlace.DataLayer.DTOs;
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
		#endregion
	}
}
