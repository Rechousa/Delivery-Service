using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryService.Database.Migrations
{
    public partial class AddedRouteNumberOfViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Routes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Routes");
        }
    }
}
