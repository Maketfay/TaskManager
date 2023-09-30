using Infrastructure.Entity;
using ViewModel;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<TokenModel?> CreateAsync(string username, string password);

        Task<TokenModel?> AuthenticateAsync(string username, string password);

        Task<TokenModel?> RefreshTokenAsync(TokenModel model);
    }
}
