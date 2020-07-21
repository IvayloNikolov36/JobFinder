namespace JobFinder.Data.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        IQueryable<T> AllAsNoTracking();

        Task<T> FindAsync<TKey>(TKey id);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChangesAsync();
    }
}
