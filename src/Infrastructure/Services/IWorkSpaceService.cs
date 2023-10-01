using Infrastructure.Entity;

namespace Infrastructure.Services
{
    public interface IWorkSpaceService
    {
        Task<IWorkSpace?> CreateAsync(Guid userId, string name);
    }
}
