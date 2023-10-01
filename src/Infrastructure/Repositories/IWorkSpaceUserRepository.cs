using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IWorkSpaceUserRepository
    {
        Task<IWorkSpaceUser> CreateAsync(IUser user, IWorkSpace workSpace);
    }
}
