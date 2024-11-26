namespace JobFinder.Data.Repositories
{
    using JobFinder.Data.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EfCoreRepository<T> : IRepository<T> where T : class
    {
        public EfCoreRepository(JobFinderDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        protected JobFinderDbContext Context { get; set; }

        public virtual IQueryable<T> All() => this.DbSet;

        public virtual IQueryable<T> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public IQueryable<T> AllWhere(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) 
            => this.DbSet.FirstOrDefaultAsync(predicate);

        public virtual Task<T> FindAsync<TKey>(TKey id) => this.DbSet.FindAsync(id).AsTask();

        public virtual Task AddAsync(T entity) => this.DbSet.AddAsync(entity).AsTask();

        public virtual Task AddRangeAsync(IEnumerable<T> entities) => this.DbSet.AddRangeAsync(entities);

        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            this.DbSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity) => this.DbSet.Remove(entity);

        public virtual void DeleteRange(IEnumerable<T> entities) => this.DbSet.RemoveRange(entities);

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entitiestoRemove = this.DbSet.Where(predicate);
            this.DbSet.RemoveRange(entitiestoRemove);
        }

        public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
