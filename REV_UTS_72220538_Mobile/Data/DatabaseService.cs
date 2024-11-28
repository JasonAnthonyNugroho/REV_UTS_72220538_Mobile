using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace REV_UTS_72220538_Mobile.Data
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<category>().Wait(); // Membuat tabel category
        }

        // Tambah Category
        public Task<int> AddCategoryAsync(category category)
        {
            return _database.InsertAsync(category);
        }

        // Ambil Semua Category
        public Task<List<category>> GetCategoriesAsync()
        {
            return _database.Table<category>().ToListAsync();
        }

        // Update Category
        public Task<int> UpdateCategoryAsync(category category)
        {
            return _database.UpdateAsync(category);
        }

        // Hapus Category
        public Task<int> DeleteCategoryAsync(int id)
        {
            return _database.DeleteAsync<category>(id);
        }
    }
}
