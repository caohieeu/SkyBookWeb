using SkyBookWeb.Core.Interfaces;
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
        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Complete()
        {
           return await _dbContext.SaveChangesAsync() == 1;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
