using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Infrastructure.MySql.Entities;

namespace Core.Infrastructure.MySql.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> query);
    }
}
