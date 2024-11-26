namespace JobFinder.Data.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        IQueryable<T> AllWhere(Expression<Func<T, bool>> predicate);

        IQueryable<T> AllAsNoTracking();

        Task<T> FindAsync<TKey>(TKey id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        void DeleteWhere(Expression<Func<T, bool>> predicate);

        Task<int> SaveChangesAsync();
    }
}
