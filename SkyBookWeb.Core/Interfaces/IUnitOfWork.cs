using SkyBookWeb.Core.Entities;

namespace SkyBookWeb.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
        IGenericRepository<T, TKey> Repository<T, TKey>() where T : BaseEntity, IEntity<TKey>;
    }
}
