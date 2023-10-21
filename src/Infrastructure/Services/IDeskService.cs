using Infrastructure.Entity;

namespace Infrastructure.Services
{
    public interface IDeskService
    {
        Task<IDesk?> CreateAsync(string name, IDeskVisibilityType deskVisibilityType, IUser user, IWorkSpace workSpace);
    }
}
