using EF_IDS.Entities;

namespace WebAPI.Repositories.Arrival
{
    public interface ILongRepository<T> : IDisposable where T : class
    {
        Task<T> CreateAsync(T c);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T> RetrieveAsync(long id);
        Task<T> UpdateAsync(long id, T c);
        Task<bool> DeleteAsync(long id);
    }
}
