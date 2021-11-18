using System.Reflection;
using Cars.Core.Base.Log;
using Cars.Sales.Core.Domain.Entities;
using Cars.Sales.Core.Infrastructure.Mappings;
using Cars.SharedKernel.Sales.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Cars.Sales.Core.Infrastructure
{
    public class SalesDbContext : DbContext
    {
        private const string MIGRATIONS_HISTORY_TABLE = "__SalesMigrationsHistory";
        private const string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True;MultipleActiveResultSets=True";
        internal const string DB_MONEY_TYPE = "money";
        public const string SCHEMA_NAME = "sales";
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new EntityFrameworkLogger() });

        public SalesDbContext() : base()
        {
        }

        public SalesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(CONNECTION_STRING, x => x.MigrationsHistoryTable(MIGRATIONS_HISTORY_TABLE));
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(SCHEMA_NAME);

            modelBuilder.ApplyConfiguration(new OfferMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderCommentMap());

            modelBuilder.Entity<OrderListViewModel>(e =>
            {
                e.HasNoKey();
                e.ToView("OrderListView");
            });
        }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderListViewModel> OrderListView { get; set; }
    }

    public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        private const string DESIGN_CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True;MultipleActiveResultSets=True";

        public SalesDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SalesDbContext>();
            builder.UseSqlServer(DESIGN_CONNECTION_STRING, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(SalesDbContext).GetTypeInfo().Assembly.GetName().Name));
            return new SalesDbContext(builder.Options);
        }
    }
}
