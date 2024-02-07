using EF_IDS.Entities;

namespace WebAPI.Repositories.Arrival
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T> CreateAsync(T c);
        Task<IEnumerable<T>> RetrieveAllAsync();
        Task<T> RetrieveAsync(int id);
        Task<T> UpdateAsync(int id, T c);
        Task<bool> DeleteAsync(int id);
    }
}
