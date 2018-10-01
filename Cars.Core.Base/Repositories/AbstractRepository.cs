using Cars.Core.Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cars.Core.Base.Repositories
{
    public abstract class AbstractRepository<TEntity, TKey, TContext> where TEntity : AggregateRoot<TKey> where TContext : DbContext
    {
        protected TContext Context { get; }
        
        protected AbstractRepository(TContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
