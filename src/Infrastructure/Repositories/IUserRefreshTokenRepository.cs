using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<IUserRefreshToken> CreateAsync(string token, DateTime expired, IUser user);

        Task<IUserRefreshToken?> ReadAsync(string token);

        Task<IUserRefreshToken?> ReadAsync(IUser user);

        Task UpdateAsync(IUserRefreshToken userRefreshToken, string token, DateTime expired);
    }
}
