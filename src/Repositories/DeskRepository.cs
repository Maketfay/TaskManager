using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public class DeskRepository: IDeskRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        public DeskRepository(ApplicationDbContext context, IServiceProvider serviceProvider) 
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IDesk> CreateAsync(string name, IDeskVisibilityType deskVisibilityType, IUser user)
        {
            var entity = _serviceProvider.GetRequiredService<IDesk>();

            entity.Id = Guid.NewGuid();
            entity.Name = name;
            entity.DeskVisibilityType = deskVisibilityType;
            entity.User = user;

            var desk = await _context.Desk.AddAsync((DeskEntity)entity);
            _context.Entry(entity.User).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            _context.Entry(entity.DeskVisibilityType).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return desk.Entity;
        }

        public async Task<IDesk?> ReadAsync(string name, IWorkSpace workSpace)
        {
            var desk =  await _context.WorkSpaceDesk
                .Include(wd => wd.Desk)
                .Include(wd => wd.Desk.DeskVisibilityType)
                .Include(wd => wd.Desk.User)
                .Include(wd => wd.WorkSpace)
                .Where(wd => wd.WorkSpace.Equals(workSpace))
                .Select(wd => wd.Desk)
                .Where(d => d.Name.Equals(name))
                .FirstOrDefaultAsync();

            return desk;
        }

        public async Task<TResult> InTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await func();

                await _context.SaveChangesAsync();

                await _context.Database.CommitTransactionAsync();

                return result;
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                return null;
            }
        }

        public async Task InTransaction(Action func)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                func.Invoke();

                await _context.Database.CommitTransactionAsync();
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
            }
        }
    }
}
