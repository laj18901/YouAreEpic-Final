using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.MongoDB
{
    public record MongoSettings(string ConnectionString, string DatabaseName);
    public class MongoDBRepository<TKey,TEntity> : IMongoDBRepository<TKey,TEntity> 
        where TKey : IEquatable<TKey>
        where TEntity : IDocument<TKey>
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly IMongoDatabase _mongoDatabase;
        public MongoDBRepository(MongoSettings settings)
        {
            _mongoDatabase = new MongoClient(settings.ConnectionString)
                                .GetDatabase(settings.DatabaseName);

            _collection = _mongoDatabase.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type entityType)
        {
            return ((BsonCollectionAttribute)entityType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public IQueryable<TEntity> AsQueryable() => _collection.AsQueryable();

        public Task DeleteByIdAsync(TKey id)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            return _collection.DeleteOneAsync(filter);
        }

        public async Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression) => await _collection.DeleteOneAsync(filterExpression);

        public async Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression) => (await _collection.FindAsync(filterExpression)).ToEnumerable();

        public async Task<TEntity> FindByIdAsync(TKey id)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);

            return await (await _collection.FindAsync(filter))
                                .FirstOrDefaultAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression) => await (await _collection.FindAsync(filterExpression))
                            .FirstOrDefaultAsync();

        public async Task<TEntity> InsertOneAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TEntity> ReplaceOneAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity);
            return entity;
        }

        public async Task DropCollectionAsync() => await _collection.Database.DropCollectionAsync(GetCollectionName(typeof(TEntity)));

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression) => await _collection.DeleteManyAsync(filterExpression);

        public async Task<List<TEntity>> InsertManyAsync(List<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
           return await (await _collection.FindAsync(e => true)).ToListAsync();
        }
    }
       
}
