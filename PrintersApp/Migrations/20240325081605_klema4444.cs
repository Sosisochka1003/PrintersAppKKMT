using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class klema4444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InventoryNumber",
                schema: "PrinterApp",
                table: "Printers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "InventoryNumber",
                schema: "PrinterApp",
                table: "Printers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
