using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Mongodb
{
    public class MongoGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoGenericRepository(MongoDbContext context)
        {
            // Use the entity type name (in lowercase) as the collection name.
            _collection = context.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public async Task<T> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T?> Delete(T entity)
        {
            // Assumes that T has an "Id" property of type Guid.
            var filter = Builders<T>.Filter.Eq("Id", entity.GetType().GetProperty("Id")!.GetValue(entity));
            await _collection.DeleteOneAsync(filter);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> Update(T entity)
        {
            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity does not contain an 'Id' property.");

            var idValue = idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", idValue);
            await _collection.ReplaceOneAsync(filter, entity);
            return entity;
        }
    }
}