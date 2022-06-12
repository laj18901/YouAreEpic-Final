using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.MongoDB
{
    public interface IMongoDBRepository<TKey,TEntity> : IRepository<TKey,TEntity> 
        where TKey : IEquatable<TKey>
        where TEntity : IDocument<TKey>
    {
        public Task DropCollectionAsync();

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);

        public Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        public Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression);

    }
    
}
