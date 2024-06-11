using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class klema : Migration
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
                    StockCount = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartridges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InventoryNumber = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projectors",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    InvenrotyNumber = table.Column<int>(type: "integer", nullable: false),
                    MileageHours = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkStations",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<int>(type: "integer", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Motherboard = table.Column<string>(type: "text", nullable: false),
                    CPU = table.Column<string>(type: "text", nullable: false),
                    GPU = table.Column<string>(type: "text", nullable: false),
                    RAMName = table.Column<string>(type: "text", nullable: false),
                    RAMVolume = table.Column<int>(type: "integer", nullable: false),
                    ROMSsdName = table.Column<string>(type: "text", nullable: false),
                    ROMSsdVolume = table.Column<int>(type: "integer", nullable: false),
                    ROMHddName = table.Column<string>(type: "text", nullable: false),
                    ROMHddVolume = table.Column<int>(type: "integer", nullable: false),
                    Monitor = table.Column<string>(type: "text", nullable: false),
                    Keyboard = table.Column<string>(type: "text", nullable: false),
                    Mouse = table.Column<string>(type: "text", nullable: false),
                    UPS = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commings",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false),
                    Location = table.Column<int>(type: "integer", nullable: false),
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
                name: "PrinterCartridges",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrinterId = table.Column<int>(type: "integer", nullable: false),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterCartridges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrinterCartridges_Cartridges_CartridgeId",
                        column: x => x.CartridgeId,
                        principalSchema: "PrinterApp",
                        principalTable: "Cartridges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrinterCartridges_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalSchema: "PrinterApp",
                        principalTable: "Printers",
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
                    Room = table.Column<string>(type: "text", nullable: true),
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
                    Room = table.Column<string>(type: "text", nullable: false),
                    CartridgeId = table.Column<int>(type: "integer", nullable: false),
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
                        name: "FK_Shipments_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalSchema: "PrinterApp",
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectorsInRooms",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Room = table.Column<string>(type: "text", nullable: false),
                    ProjectorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectorsInRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectorsInRooms_Projectors_ProjectorId",
                        column: x => x.ProjectorId,
                        principalSchema: "PrinterApp",
                        principalTable: "Projectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairSessions",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkStationId = table.Column<int>(type: "integer", nullable: false),
                    DateRepair = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DescriptionRepair = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairSessions_WorkStations_WorkStationId",
                        column: x => x.WorkStationId,
                        principalSchema: "PrinterApp",
                        principalTable: "WorkStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkStationsInRooms",
                schema: "PrinterApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Room = table.Column<string>(type: "text", nullable: false),
                    WorkStationId = table.Column<int>(type: "integer", nullable: false),
                    WorkStationStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStationsInRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStationsInRooms_WorkStations_WorkStationId",
                        column: x => x.WorkStationId,
                        principalSchema: "PrinterApp",
                        principalTable: "WorkStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commings_CartridgeId",
                schema: "PrinterApp",
                table: "Commings",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterCartridges_CartridgeId",
                schema: "PrinterApp",
                table: "PrinterCartridges",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterCartridges_PrinterId",
                schema: "PrinterApp",
                table: "PrinterCartridges",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterInRooms_PrinterId",
                schema: "PrinterApp",
                table: "PrinterInRooms",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectorsInRooms_ProjectorId",
                schema: "PrinterApp",
                table: "ProjectorsInRooms",
                column: "ProjectorId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairSessions_WorkStationId",
                schema: "PrinterApp",
                table: "RepairSessions",
                column: "WorkStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CartridgeId",
                schema: "PrinterApp",
                table: "Shipments",
                column: "CartridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_PrinterId",
                schema: "PrinterApp",
                table: "Shipments",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationsInRooms_WorkStationId",
                schema: "PrinterApp",
                table: "WorkStationsInRooms",
                column: "WorkStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commings",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "PrinterCartridges",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "PrinterInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "ProjectorsInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "RepairSessions",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Shipments",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "WorkStationsInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Projectors",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Cartridges",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Printers",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "WorkStations",
                schema: "PrinterApp");
        }
    }
}
