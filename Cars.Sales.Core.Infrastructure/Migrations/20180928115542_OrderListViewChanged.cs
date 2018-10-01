using Cars.SharedKernel.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Sales.Core.Infrastructure.Migrations
{
    public partial class OrderListViewChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ExecuteSqlEmbeddedResourceScript<OrderListViewChanged>(MigrationType.Up);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.ExecuteSqlEmbeddedResourceScript<OrderListViewChanged>(MigrationType.Down);
        }
    }
}
