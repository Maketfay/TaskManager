using Infrastructure.Entity;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<IUser?> CreateAsync(string username, string password);
    }
}
