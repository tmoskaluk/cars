using Cars.SharedKernel.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Sales.Core.Infrastructure.Migrations
{
    public partial class OrderListViewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ExecuteSqlEmbeddedResourceScript<OrderListViewAdded>(MigrationType.Up);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DROP VIEW {SalesDbContext.SCHEMA_NAME}.OrderListView");
        }
    }
}
