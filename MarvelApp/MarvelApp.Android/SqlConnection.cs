using MarvelApp.Droid;
using MarvelApp.Services.Interfaces;
using SQLite;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqlConnection))]
namespace MarvelApp.Droid
{
    public class SqlConnection : ISqlConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "data.db");

            return new SQLiteAsyncConnection(dbPath);
        }
    }
}