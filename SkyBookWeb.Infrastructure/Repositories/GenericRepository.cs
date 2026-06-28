using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyBookWeb.Core.Entities;
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch(Exception ex)
            {
                var logger = _loggerFactory.CreateLogger<GenericRepository<T>>();
                logger.LogError(ex, "Something went wrong with get data from db");

                return null;
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

        public bool Delete(T entity)
        {
            return true;
        }
    }
}
