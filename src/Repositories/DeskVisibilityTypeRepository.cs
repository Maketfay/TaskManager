using Infrastructure.Entity;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class DeskVisibilityTypeRepository: IDeskVisibilityTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public DeskVisibilityTypeRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<IDeskVisibilityType>> ReadAllAsync()
        {
            return await _context.DeskVisibilityType.ToListAsync();
        }

        public async Task<IDeskVisibilityType?> ReadAsync(string code)
        {
            return await _context.DeskVisibilityType.FirstOrDefaultAsync(dvt => dvt.Code.Equals(code));
        }
    }
}
