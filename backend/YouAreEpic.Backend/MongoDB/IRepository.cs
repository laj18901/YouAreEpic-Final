using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.MongoDB
{
    public interface IRepository<TKey,TEntity>
         where TEntity : IDocument<TKey>
         where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> AsQueryable();

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> FindByIdAsync(TKey id);

        Task DeleteByIdAsync(TKey id);

        Task<TEntity> InsertOneAsync(TEntity document);

        Task<List<TEntity>> InsertManyAsync(List<TEntity> entities);

        Task<TEntity> ReplaceOneAsync(TEntity document);

    }
}
