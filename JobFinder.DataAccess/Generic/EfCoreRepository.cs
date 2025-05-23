using AutoMapper;
using JobFinder.Common.Exceptions;
using JobFinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace JobFinder.DataAccess.Generic
{
    public class EfCoreRepository<T> : IRepository<T> where T : class
    {
        public EfCoreRepository(JobFinderDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        protected JobFinderDbContext Context { get; set; }

        public virtual IQueryable<T> All() => DbSet;

        public virtual IQueryable<T> DbSetNoTracking() => DbSet.AsNoTracking();

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) 
            => DbSet.FirstOrDefaultAsync(predicate);

        public virtual Task<T> FindAsync<TKey>(TKey id) => DbSet.FindAsync(id).AsTask();

        public virtual Task AddAsync(T entity) => DbSet.AddAsync(entity).AsTask();

        public virtual Task AddRangeAsync(IEnumerable<T> entities) => DbSet.AddRangeAsync(entities);

        public virtual void Update(T entity)
        {
            EntityEntry<T> entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity) => DbSet.Remove(entity);

        public virtual void DeleteRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities);

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entitiestoRemove = DbSet.Where(predicate);
            DbSet.RemoveRange(entitiestoRemove);
        }

        public Task<int> SaveChangesAsync() => Context.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void ValidateForExistence(object entity, string entityName)
        {
            if (entity is null)
            {
                throw new ActionableException($"Entity {entityName} does not exist!");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context?.Dispose();
            }
        }
    }
}
