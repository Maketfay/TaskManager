using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IDeskRepository
    {
        Task<IDesk> CreateAsync(string name, IDeskVisibilityType deskVisibilityType, IUser user);
        Task<IDesk?> ReadAsync(string name, IWorkSpace workSpace);
        Task<TResult> InTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
        Task InTransaction(Action func);
    }
}
