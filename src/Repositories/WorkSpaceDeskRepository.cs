using Entity;
using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Repositories
{
    public class WorkSpaceDeskRepository: IWorkSpaceDeskRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IServiceProvider _serviceProvider;

        public WorkSpaceDeskRepository(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<IWorkSpaceDesk> CreateAsync(IWorkSpace workSpace, IDesk desk)
        {
            var entity = _serviceProvider.GetRequiredService<IWorkSpaceDesk>();

            entity.Id = Guid.NewGuid();
            entity.Desk = desk;
            entity.WorkSpace = workSpace;

            var workSpaceDesk = await _context.WorkSpaceDesk.AddAsync((WorkSpaceDeskEntity)entity);

            _context.Entry(entity.Desk).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            _context.Entry(entity.WorkSpace).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            await _context.SaveChangesAsync();

            return workSpaceDesk.Entity;
        }
        public async IAsyncEnumerable<IDesk>? ReadCollectionAsync(IWorkSpace workSpace)
        {
           await foreach (var desk in _context.WorkSpaceDesk
                .Include(wsd => wsd.Desk)
                .Include(wsd => wsd.WorkSpace)
                .Where(wsd => wsd.WorkSpace.Equals(workSpace))
                .Select(wsd => wsd.Desk)
                .AsAsyncEnumerable()) 
            {
                yield return desk;
            }
        }
    }
}
