using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}
