using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class klema3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Printers_Printer",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_Printer",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Count",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Printer",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_PrinterId",
                schema: "PrinterApp",
                table: "Shipments",
                column: "PrinterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Printers_PrinterId",
                schema: "PrinterApp",
                table: "Shipments",
                column: "PrinterId",
                principalSchema: "PrinterApp",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Printers_PrinterId",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_PrinterId",
                schema: "PrinterApp",
                table: "Shipments");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                schema: "PrinterApp",
                table: "Shipments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Printer",
                schema: "PrinterApp",
                table: "Shipments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_Printer",
                schema: "PrinterApp",
                table: "Shipments",
                column: "Printer");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Printers_Printer",
                schema: "PrinterApp",
                table: "Shipments",
                column: "Printer",
                principalSchema: "PrinterApp",
                principalTable: "Printers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
