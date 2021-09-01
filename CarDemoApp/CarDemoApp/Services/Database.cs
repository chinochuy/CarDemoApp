using CarDemoApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarDemoApp.Services
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CarModel>().Wait();
        }

        public Task<List<CarModel>> GetCars()
        {
            return _database.Table<CarModel>().ToListAsync();
        }

        public Task<int> SaveCars(CarModel car)
        {
            return _database.InsertAsync(car);
        }
    }
}
