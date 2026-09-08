using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Infrastructure.Data;

namespace SkyBookWeb.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILoggerFactory _loggerFactory;
        public GenericRepository(ApplicationDBContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;

            _loggerFactory = loggerFactory;
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>? expression = null)
        {
            try
            {
                var query = _dbContext.Set<T>().AsQueryable<T>();
                if(expression != null)
                {
                    query = query.Include(expression);
                }
                return await query.ToListAsync();
            }
            catch(Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<GenericRepository<T>>();
                logger.LogError(ex, "Something went wrong with get data from db");

                return Enumerable.Empty<T>();
            }
        }

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task<T> GetIdAsync(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<T> GetEntityWithSpec(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).ToListAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }

        public async Task<int> CountAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).CountAsync();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
