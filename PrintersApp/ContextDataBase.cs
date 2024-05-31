using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;

namespace PrintersApp
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка подключения к базе данных");
                System.Windows.Application.Current.Shutdown();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PrinterApp");
        }

        public DbSet<Cartridge> Cartridges { get; set; }
        public DbSet<PrinterCartridge> PrinterCartridges { get; set; }
        public DbSet<PrinterInRoom> PrinterInRooms { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Comming> Commings { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Projector> Projectors { get; set; }
        public DbSet<ProjectorsInRoom> ProjectorsInRooms { get; set; }
        public DbSet<WorkStation> WorkStations { get; set; }
        public DbSet<WorkStationsInRoom> WorkStationsInRooms { get; set; }
        public DbSet<RepairSession> RepairSessions { get; set; }

        public enum VarLocation
        {
            Первый,
            Второй,
            ККМТ,
            ТТД,
            Общий
        }

        public class Printer
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public required string Name { get; set; }
            public required string InventoryNumber { get; set; }
            public VarLocation Location { get; set; }
        }

        public class PrinterInRoom
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string? Room { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public required Printer PrinterObject { get; set; }
        }

        public class Cartridge
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public required string Name { get; set; }
            public int StockCount { get; set; }
            public VarLocation Location { get; set; }
        }

        public class Comming
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public required Cartridge CartridgeObject { get; set; }
            public required VarLocation Location { get; set; }
            public int Count { get; set; }
            public required DateTime CommingDate { get; set; }
        }

        public class Shipment
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public required Printer PrinterObject { get; set; }
            public required string Room { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public required Cartridge CartridgeObject { get; set; }
            public DateTime ShipmentDate { get; set; }
        }

        public class PrinterCartridge
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public required Printer Printer { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public required Cartridge Cartridge { get; set; }
        }

        public class Projector
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Model { get; set; }
            public VarLocation Location { get; set; }
            public int InvenrotyNumber { get; set; }
            public string MileageHours { get; set; }
        }

        public class ProjectorsInRoom
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Room { get; set; }
            [ForeignKey(nameof(Projector))]
            public int ProjectorId { get; set; }
            public required Projector ProjectorObject { get; set; }
        }

        public class WorkStation
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public VarLocation Location { get; set; }
            public string Brand { get; set; }
            public string Motherboard { get; set; }
            public string CPU { get; set; }
            public string GPU { get; set; }
            public string RAMName { get; set; }
            public int RAMVolume { get; set; }
            [AllowNull]
            public string ROMSsdName { get; set; }
            [AllowNull]
            public int ROMSsdVolume { get; set; }
            [AllowNull]
            public string ROMHddName { get; set; }
            [AllowNull]
            public int ROMHddVolume { get; set; }
            public string Monitor { get; set; }
            public string Keyboard { get; set; }
            public string Mouse { get; set; }
            [AllowNull]
            public string UPS { get; set; }
        }

        public enum Status
        {
            Work,
            NeedRepair,
            OutOfUse
        }

        public class WorkStationsInRoom
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Room { get; set; }
            [ForeignKey(nameof(WorkStation))]
            public int WorkStationId { get; set; }
            public WorkStation WorkStationObject { get; set; }
            public Status WorkStationStatus { get; set; }
        }

        public class RepairSession
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(WorkStation))]
            public int WorkStationId { get; set; }
            public WorkStation WorkStationObject { get; set; }
            public DateTime DateRepair { get; set; }
            public string DescriptionRepair { get; set; }
        }
    }
}
