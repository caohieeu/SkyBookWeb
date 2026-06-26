using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SkyBookWeb.Core.Entities;
using SkyBookWeb.Core.Interfaces;
using SkyBookWeb.Core.Specifications;
using SkyBookWeb.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILoggerFactory _loggerFactory;
        private Dictionary<object, object> _repositories;
        public UnitOfWork(ApplicationDBContext dbContext,
            ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _loggerFactory = loggerFactory;
            _repositories = new Dictionary<object, object>();
        }
        public async Task<bool> Complete()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(Type))
            {
                var typeGeneric = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                        typeGeneric.MakeGenericType(typeof(TEntity)),
                        _dbContext,
                        _loggerFactory
                    );
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }
    }
}
