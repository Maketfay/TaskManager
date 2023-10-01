using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public class WorkSpaceUserRepository: IWorkSpaceUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;
        public WorkSpaceUserRepository(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IWorkSpaceUser> CreateAsync(IUser user, IWorkSpace workSpace) 
        {
            var entity = _serviceProvider.GetRequiredService<IWorkSpaceUser>();

            entity.Id = Guid.NewGuid();
            entity.User = user;
            entity.WorkSpace = workSpace;

            var workSpaceUser = await _context.WorkSpaceUser.AddAsync((WorkSpaceUserEntity)entity);

            _context.Entry(entity.User).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            _context.Entry(entity.WorkSpace).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return workSpaceUser.Entity;
        }
    }
}
