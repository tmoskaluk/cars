using Cars.Core.Base.Log;
using Cars.Customers.Crud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Cars.Customers.Crud
{
    public class CustomersDbContext : DbContext
    {
        private const string MIGRATIONS_HISTORY_TABLE = "__CustomersMigrationsHistory";
        private const string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True;MultipleActiveResultSets=True";
        public const string SCHEMA_NAME = "customers";
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new EntityFrameworkLogger() });

        public CustomersDbContext() : base()
        {
        }

        public CustomersDbContext(DbContextOptions options) : base(options)
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
            modelBuilder.ApplyConfiguration(new CustomerMap());
        }

        public DbSet<Customer> Customers { get; set; }
    }

    public class CustomersDbContextFactory : IDesignTimeDbContextFactory<CustomersDbContext>
    {
        private const string DESIGN_CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True;MultipleActiveResultSets=True";

        public CustomersDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CustomersDbContext>();
            builder.UseSqlServer(DESIGN_CONNECTION_STRING, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(CustomersDbContext).GetTypeInfo().Assembly.GetName().Name));
            return new CustomersDbContext(builder.Options);
        }
    }
}
