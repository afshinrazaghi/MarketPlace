using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services
{
    public class UserRepository : IUserRepository
    {
        #region Constructor
        private readonly IGenericRepository<User> _userRepository;

        public UserRepository(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
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
