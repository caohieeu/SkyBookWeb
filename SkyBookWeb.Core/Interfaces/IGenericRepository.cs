using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Specifications;

namespace SkyBookWeb.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? expression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<T> GetEntityWithSpec(ISpecifications<T> specifications);
        Task<IEnumerable<T>> ListAsync(ISpecifications<T> specifications);
        IQueryable<T> ApplySpecification(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
    }
}
