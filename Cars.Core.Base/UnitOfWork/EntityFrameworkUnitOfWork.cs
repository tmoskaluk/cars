using Microsoft.EntityFrameworkCore;

namespace Cars.Core.Base.UnitOfWork
{
    public abstract class EntityFrameworkUnitOfWork<TContext> where TContext : DbContext
    {
        protected TContext Context { get; }

        protected EntityFrameworkUnitOfWork(TContext context)
        {
            Context = context;
        }

        public virtual void Commit()
        {
            this.Context.SaveChanges();
        }
    }
}
