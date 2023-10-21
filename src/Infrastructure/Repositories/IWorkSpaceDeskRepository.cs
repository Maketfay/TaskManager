using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IWorkSpaceDeskRepository
    {
        Task<IWorkSpaceDesk> CreateAsync(IWorkSpace workSpace, IDesk desk);

        IAsyncEnumerable<IDesk>? ReadCollectionAsync(IWorkSpace workSpace);
    }
}
