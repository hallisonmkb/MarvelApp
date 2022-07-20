using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelApp.Services.Interfaces
{
    public interface IDataSource<T> where T : class, new()
    {
        Task<int> AddAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAsync();
    }
}
