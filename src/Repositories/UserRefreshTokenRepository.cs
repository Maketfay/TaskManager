using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public class UserRefreshTokenRepository: IUserRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        public UserRefreshTokenRepository(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IUserRefreshToken> CreateAsync(string token, DateTime expired, IUser user) 
        {
            var entity = _serviceProvider.GetRequiredService<IUserRefreshToken>();
            
            entity.Id = Guid.NewGuid();
            entity.RefreshToken = token;
            entity.Expired = expired;
            entity.User = user;

            var userRefreshToken = await _context.UserRefreshToken.AddAsync((UserRefreshTokenEntity)entity);
            _context.Entry(entity.User).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return userRefreshToken.Entity;
        }

        public async Task<IUserRefreshToken?> ReadAsync(string token) 
        {
            return await _context.UserRefreshToken
                .Include(urt => urt.User)
                .FirstOrDefaultAsync(urt => urt.RefreshToken.Equals(token)); 
        }
        public async Task UpdateAsync(IUserRefreshToken userRefreshToken, string token, DateTime expired) 
        {
            userRefreshToken.RefreshToken = token;
            userRefreshToken.Expired = expired;

            await _context.SaveChangesAsync();
        }

        public async Task<IUserRefreshToken?> ReadAsync(IUser user) 
        {
            return await _context.UserRefreshToken
                .Include(urt => urt.User)
                .FirstOrDefaultAsync(urt => urt.User.Equals(user));
        }
    }
}
