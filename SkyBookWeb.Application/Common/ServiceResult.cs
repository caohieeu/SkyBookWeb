using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyBookWeb.Application.Common
{
    public abstract record ServiceResult<TEntity> where TEntity : class
    {
        public sealed record Success(TEntity? entity) : ServiceResult<TEntity>;

        public sealed record Failure(string Errors) : ServiceResult<TEntity>;

        public static ServiceResult<TEntity> FromSuccess(TEntity? entity = null) 
            => new Success(entity);

        public static ServiceResult<TEntity> FromFailure(string errors)
            => new Failure(errors);
    }
}
