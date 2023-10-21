using Infrastructure.Entity;

namespace Infrastructure.Repositories
{
    public interface IDeskVisibilityTypeRepository
    {
        Task<IDeskVisibilityType?> ReadAsync(string code);

        Task<IEnumerable<IDeskVisibilityType>> ReadAllAsync();
    }
}
