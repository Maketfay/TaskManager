using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IWorkSpaceRepository
    {
        Task<IWorkSpace> CreateAsync(string name);
    }
}
