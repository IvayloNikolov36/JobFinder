using System.Linq.Expressions;

namespace JobFinder.DataAccess.Generic
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IQueryable<T> DbSetNoTracking();

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

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
