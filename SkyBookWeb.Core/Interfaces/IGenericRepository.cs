using System.Linq.Expressions;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Core.Interfaces
{
    public interface IGenericRepository<T, TKey> where T : class, IEntity<TKey>
    {
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIdAsync(TKey id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<int> SaveChangeAsync();
        Task<T> GetEntityWithSpec(ISpecifications<T> specifications);
        Task<IEnumerable<T>> ListAsync(ISpecifications<T> specifications);
        IQueryable<T> ApplySpecification(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
    }
}
