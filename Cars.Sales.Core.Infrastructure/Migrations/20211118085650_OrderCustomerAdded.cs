using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Sales.Core.Infrastructure.Migrations
{
    public partial class OrderCustomerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Customer_CustomerId",
                schema: "sales",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                schema: "sales",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_CustomerId",
                schema: "sales",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                schema: "sales",
                table: "Orders");
        }
    }
}
