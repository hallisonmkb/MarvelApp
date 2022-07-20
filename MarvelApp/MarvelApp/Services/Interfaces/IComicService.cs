using MarvelApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelApp.Services.Interfaces
{
    public interface IComicService
    {
        Task<IEnumerable<Comic>> GetAsync();
        Task<IEnumerable<Comic>> GetByTitleAsync(string title);
        Task<IEnumerable<Comic>> GetByIdAsync(int id);
        Task<IEnumerable<Comic>> GetNewOnSaleAsync();
    }
}
