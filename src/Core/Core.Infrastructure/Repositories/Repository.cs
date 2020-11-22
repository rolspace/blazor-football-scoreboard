using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        protected readonly GamesContext _dbContext;

        public Repository()
            : this(new GamesContext()) { }

        public Repository(GamesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> query)
        {
            var result = _dbContext.Set<T>().AsQueryable().Where(query);
            return await result.ToListAsync();
        }
    }
}
