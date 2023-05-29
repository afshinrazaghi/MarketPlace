using MarketPlace.Application.Services.Interfaces;

namespace MarketPlace.Application.Services.Implementations
{
	public class PasswordHelper : IPasswordHelper
	{
		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool VerifyPassword(string password, string hash)
		{
			return BCrypt.Net.BCrypt.Verify(password, hash);
		}
	}
}
