using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
    }
}
