using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrintersApp.Migrations
{
    /// <inheritdoc />
    public partial class asdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_WorkStationsInRooms_WorkStationId",
                schema: "PrinterApp",
                table: "WorkStationsInRooms",
                column: "WorkStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectorsInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "RepairSessions",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "WorkStationsInRooms",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "Projectors",
                schema: "PrinterApp");

            migrationBuilder.DropTable(
                name: "WorkStations",
                schema: "PrinterApp");
        }
    }
}
