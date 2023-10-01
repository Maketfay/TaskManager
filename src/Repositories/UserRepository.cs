using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        public UserRepository(ApplicationDbContext context, IServiceProvider serviceProvider) 
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        public async Task<IUser> CreateAsync(string name, string passwordHash)
        {
            var entity = _serviceProvider.GetRequiredService<IUser>();
            entity.Id = Guid.NewGuid();
            entity.Name = name;
            entity.PasswordHash = passwordHash;

            var user = await _context.User.AddAsync((UserEntity)entity);

            await _context.SaveChangesAsync();

            return user.Entity;
        }

        public async Task<IUser?> ReadAsync(string name) 
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Name.Equals(name));
        }

        public async Task<IUser?> ReadAsync(Guid id) 
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }
    }
}
