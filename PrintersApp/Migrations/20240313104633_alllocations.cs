using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class alllocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Location",
                schema: "PrinterApp",
                table: "Printers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                schema: "PrinterApp",
                table: "Cartridges",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "PrinterApp",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "PrinterApp",
                table: "Cartridges");
        }
    }
}
