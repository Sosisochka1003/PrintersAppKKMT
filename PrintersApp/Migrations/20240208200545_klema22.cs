using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class klema22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                schema: "PrinterApp",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                schema: "PrinterApp",
                table: "Shipments");
        }
    }
}
