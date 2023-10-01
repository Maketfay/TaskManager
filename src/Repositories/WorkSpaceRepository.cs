using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public class WorkSpaceRepository: IWorkSpaceRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        public WorkSpaceRepository(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IWorkSpace> CreateAsync(string name) 
        {
            var entity = _serviceProvider.GetRequiredService<IWorkSpace>();

            entity.Id = Guid.NewGuid();
            entity.Name = name;

            var workSpace = await _context.WorkSpace.AddAsync((WorkSpaceEntity)entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
