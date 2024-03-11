﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class klema1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PrinterApp");

            migrationBuilder.CreateTable(
                name: "Cartridges",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StockCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commings",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    CommingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commings_Cartridges_CartridgeId",
                        column: x => x.CartridgeId,
                        principalSchema: "PrinterApp",
                        principalTable: "Cartridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printers_Cartridges_CartridgeId",
                        column: x => x.CartridgeId,
                        principalSchema: "PrinterApp",
                        principalTable: "Cartridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrinterInRooms",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Room = table.Column<string>(type: "text", nullable: false),
                    PrinterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterInRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrinterInRooms_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalSchema: "PrinterApp",
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrinterId = table.Column<int>(type: "integer", nullable: false),
                    Printer = table.Column<int>(type: "integer", nullable: false),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ShipmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Cartridges_CartridgeId",
                        column: x => x.CartridgeId,
                        principalSchema: "PrinterApp",
                        principalTable: "Cartridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Printers_Printer",
                        column: x => x.Printer,
                        principalSchema: "PrinterApp",
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commings_CartridgeId",
                schema: "PrinterApp",
                table: "Commings",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterInRooms_PrinterId",
                schema: "PrinterApp",
                table: "PrinterInRooms",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_CartridgeId",
                schema: "PrinterApp",
                table: "Printers",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CartridgeId",
                schema: "PrinterApp",
                table: "Shipments",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_Printer",
                schema: "PrinterApp",
                table: "Shipments",
                column: "Printer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commings",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "PrinterInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Shipments",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Printers",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Cartridges",
                schema: "PrinterApp");
        }
    }
}
