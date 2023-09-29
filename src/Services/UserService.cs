using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IUser?> CreateAsync(string username, string password) 
        {
            var user = await _userRepository.ReadAsync(username);

            if (user is null)
            {
                var passwordHash = Hash(password);

                user = await _userRepository.CreateAsync(username, passwordHash);
            }
            else
                return null;

            return user;
        }

        private string Hash(string password)
        {
            SHA512 hash = SHA512.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
        }
    }
}
