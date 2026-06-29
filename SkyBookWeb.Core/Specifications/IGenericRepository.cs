using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Specifications
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
