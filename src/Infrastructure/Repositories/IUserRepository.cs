using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> CreateAsync(string name, string passwordHash);
        Task<IUser?> ReadAsync(string name);
    }
}
