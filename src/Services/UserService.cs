using Infrastructure.Entity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ViewModel;

namespace Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;

        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<TokenModel?> CreateAsync(string username, string password) 
        {
            var user = await _userRepository.ReadAsync(username);

            if (user is null)
            {
                var passwordHash = Hash(password);

                user = await _userRepository.CreateAsync(username, passwordHash);
            }
            else
                return null;

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            int refreshTokenExpirationTimeInMinutes = int.Parse(_configuration["RefreshTokenSettings:ExpirationTimeMin"]);
            await _userRefreshTokenRepository.CreateAsync(refreshToken, DateTime.UtcNow.AddMinutes(refreshTokenExpirationTimeInMinutes), user);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenModel?> AuthenticateAsync(string username, string password) 
        {
            var user = await _userRepository.ReadAsync(username);
            if (user is null)
                return null;

            var passwordHash = Hash(password);

            if (!passwordHash.Equals(user.PasswordHash))
                return null;

            var userRefreshToken = await _userRefreshTokenRepository.ReadAsync(user);

            return await UpdateTokenAsync(userRefreshToken);
        }

        public async Task<TokenModel?> RefreshTokenAsync(TokenModel model) 
        {
            var userRefreshToken = await _userRefreshTokenRepository.ReadAsync(model.RefreshToken);

            var isTokenValid = ValidateToken(model.AccessToken, userRefreshToken);

            if (!isTokenValid)
                return null;

            return await UpdateTokenAsync(userRefreshToken);
           
        }
        private string Hash(string password)
        {
            SHA512 hash = SHA512.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
        }

        private string GenerateAccessToken(IUser user) 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.Name),
            };


            int accessTokenExpirationTimeInMinutes = int.Parse(_configuration["AccessTokenSettings:ExpirationTimeMin"]);

            var accessToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private JwtSecurityToken? ParseJwt(string jwtToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtSecurityToken;
                jwtSecurityToken = handler.ReadJwtToken(jwtToken);
                return jwtSecurityToken;
            }
            catch
            {
                return null;
            }
        }

        private bool ValidateToken(string accessToken, IUserRefreshToken userRefreshToken) 
        {
            if (userRefreshToken is null)
                return false;

            var jwtSecurityToken = ParseJwt(accessToken);
            if (jwtSecurityToken is null)
            {
                return false;
            }

            var claim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sid));
            if (claim == null)
                return false;

            if (!Guid.TryParse(claim.Value, out Guid userId))
                return false;

            if (userId != userRefreshToken.User.Id)
                return false;

            var isRefreshTokenExpired = DateTime.Now >= userRefreshToken.Expired;

            if (isRefreshTokenExpired)
                return false;

            return true;
        }

        private async Task<TokenModel> UpdateTokenAsync(IUserRefreshToken userRefreshToken) 
        {
             var accessToken = GenerateAccessToken(userRefreshToken.User);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpirationTimeInMinutes = int.Parse(_configuration["RefreshTokenSettings:ExpirationTimeMin"]);
            var expired = DateTime.UtcNow.AddMinutes(refreshTokenExpirationTimeInMinutes);

            await _userRefreshTokenRepository.UpdateAsync(userRefreshToken, refreshToken, expired);

            return new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
        }
    }
}
