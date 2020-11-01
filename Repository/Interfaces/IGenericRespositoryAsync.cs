using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogApi.Repository.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<IReadOnlyList<T>> GetAllAsync();
    }
}
