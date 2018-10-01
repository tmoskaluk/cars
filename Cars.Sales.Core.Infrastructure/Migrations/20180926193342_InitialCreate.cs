using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Sales.Core.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    Configuration_Model = table.Column<string>(maxLength: 50, nullable: true),
                    Configuration_Engine_Code = table.Column<string>(nullable: false),
                    Configuration_Engine_Type = table.Column<int>(nullable: false),
                    Configuration_Engine_Capacity = table.Column<int>(nullable: false),
                    Configuration_Gearbox_Gears = table.Column<int>(nullable: false),
                    Configuration_Gearbox_Type = table.Column<int>(nullable: false),
                    Configuration_Version = table.Column<int>(nullable: false),
                    Configuration_Color = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Configuration_Model = table.Column<string>(nullable: true),
                    Configuration_Engine_Code = table.Column<string>(nullable: true),
                    Configuration_Engine_Type = table.Column<int>(nullable: false),
                    Configuration_Engine_Capacity = table.Column<int>(nullable: false),
                    Configuration_Gearbox_Gears = table.Column<int>(nullable: false),
                    Configuration_Gearbox_Type = table.Column<int>(nullable: false),
                    Configuration_Version = table.Column<int>(nullable: false),
                    Configuration_Color = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PlannedDeliveryDate = table.Column<DateTime>(nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: true),
                    OriginalPrice = table.Column<decimal>(type: "money", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "sales");
        }
    }
}
