using SQLite;

namespace MarvelApp.Services.Interfaces
{
    public interface ISqlConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}
