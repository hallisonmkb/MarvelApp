using MarvelApp.Helpers;
using MarvelApp.Models;
using MarvelApp.Services;
using MarvelApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ComicApiService))]
namespace MarvelApp.Services
{
    public class ComicApiService : ApiService, IComicService
    {
        public Task<IEnumerable<Comic>> GetAsync()
        {
            var endpoint = GetEndpoint(key: ApiConstants.COMICS, settings: true);
            return HttpClientService.GetAsync<IEnumerable<Comic>>(endpoint, caching: true);
        }

        public Task<IEnumerable<Comic>> GetByTitleAsync(string title)
        { 
            var endpoint = GetEndpoint(key: ApiConstants.COMICS_TITLE + title, settings: true);
            return HttpClientService.GetAsync<IEnumerable<Comic>>(endpoint);
        }

        public Task<IEnumerable<Comic>> GetByIdAsync(int id)
        { 
            var endpoint = GetEndpoint(key: ApiConstants.COMICS_ID + id);
            return HttpClientService.GetAsync<IEnumerable<Comic>>(endpoint);
        }

        public Task<IEnumerable<Comic>> GetNewOnSaleAsync()
        { 
            var endpoint = GetEndpoint(key: ApiConstants.NEW_COMICS_ON_SALE, settings: true);
            return HttpClientService.GetAsync<IEnumerable<Comic>>(endpoint, caching: true);
        }
    }
}