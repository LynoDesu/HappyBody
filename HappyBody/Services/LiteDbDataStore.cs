using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappyBody.Core.Models;
using LiteDB;
using Xamarin.Forms;

namespace HappyBody.Services
{
    public class LiteDbDataStore : IDataStore<Meal>
    {
        protected LiteCollection<Meal> _collection;

        public LiteDbDataStore()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Meal>().Id(x => x.Id);

            var db = new LiteDatabase(DependencyService.Get<IDatabaseAccess>().DatabasePath());
            _collection = db.GetCollection<Meal>();
        }

        public Task<bool> AddItemAsync(Meal item)
        {
            var success = true;

            try
            {
                _collection.Insert(item);
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Task.FromResult(success);
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            var success = true;

            try
            {
                _collection.Delete(i => i.Id == id);
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Task.FromResult(success);
        }

        public Task<Meal> GetItemAsync(Guid id)
        {
            var meal = _collection.FindOne(i => i.Id == id);

            return Task.FromResult(meal);
        }

        public Task<IEnumerable<Meal>> GetItemsAsync(bool forceRefresh = false)
        {
            return Task.FromResult(_collection.FindAll());
        }

        public Task<bool> SaveItemAsync(Meal item)
        {
            var success = true;

            try
            {
                _collection.Upsert(item);
            }
            catch (Exception ex)
            {
                success = false;
            }

            return Task.FromResult(success);
        }
    }
}
