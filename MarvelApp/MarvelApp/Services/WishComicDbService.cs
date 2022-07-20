using MarvelApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using MarvelApp.Services;
using MarvelApp.Services.Interfaces;

[assembly: Dependency(typeof(WishComicDbService))]
namespace MarvelApp.Services
{
    public class WishComicDbService : IDataSource<WishComic>
    {
        private static SQLiteAsyncConnection db;

        static async Task Init()
        {
            db = DependencyService.Get<ISqlConnection>().GetConnection();

            await db.CreateTableAsync<WishComic>();
        }

        public async Task<int> AddAsync(WishComic item)
        {
            await Init();

            return await db.InsertAsync(item);
        }

        public async Task<int> DeleteAsync(int id)
        {
            await Init();

            return await db.DeleteAsync<WishComic>(id);
        }

        public async Task<IEnumerable<WishComic>> GetAsync()
        {
            await Init();

            return await db.Table<WishComic>().ToListAsync();
        }

        public async Task<WishComic> GetAsync(int id)
        {
            await Init();

            return await db.Table<WishComic>().FirstOrDefaultAsync(x => x.ComicId == id);
        }

        public Task<int> UpdateAsync(WishComic item)
        {
            throw new NotImplementedException();
        }
    }
}
