using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TEDDY.Models;
using Microsoft.Maui.Storage;

namespace TEDDY.Database
{
    public class AppDatabase
    {
        private static AppDatabase _instance;
        private readonly SQLiteAsyncConnection _database;

        // Private constructor to initialize the database connection
        private AppDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bankingapp.db");
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeDatabase();
        }

        // Public property to get the singleton instance
        public static AppDatabase Instance => _instance ??= new AppDatabase();

        // Asynchronous initialization of database tables
        private async void InitializeDatabase()
        {
            try
            {
                await _database.CreateTableAsync<Profile>();
                await _database.CreateTableAsync<ShoppingItem>();
                await _database.CreateTableAsync<ShoppingCart>();
            }
            catch (Exception ex)
            {
                // Handle exceptions here (e.g., log them)
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        // Profile CRUD operations
        public Task<int> SaveProfileAsync(Profile profile) => _database.InsertOrReplaceAsync(profile);
        public Task<Profile> GetProfileAsync() => _database.Table<Profile>().FirstOrDefaultAsync();

        // Shopping Item CRUD operations
        public Task<int> SaveShoppingItemAsync(ShoppingItem item) => _database.InsertOrReplaceAsync(item);
        public Task<List<ShoppingItem>> GetShoppingItemsAsync() => _database.Table<ShoppingItem>().ToListAsync();

        // Shopping Cart CRUD operations
        public Task<int> AddToCartAsync(ShoppingCart cartItem) => _database.InsertAsync(cartItem);
        public Task<List<ShoppingCart>> GetShoppingCartItemsAsync() => _database.Table<ShoppingCart>().ToListAsync();
        public Task<int> RemoveFromCartAsync(ShoppingCart cartItem) => _database.DeleteAsync(cartItem);
    }
}
