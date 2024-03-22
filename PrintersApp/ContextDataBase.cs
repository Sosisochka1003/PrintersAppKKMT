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

namespace PrintersApp
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase() 
        {
            Database.EnsureCreated();
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

        public enum VarLocation
        {
            Первый,
            Второй,
            ККМТ,
            ТТД
        }

        public class Printer
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public required string Name { get; set; }
            public Int64 InventoryNumber { get; set; }
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
            public DateTime ShipmentDate { get; set;}
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
    }
}
