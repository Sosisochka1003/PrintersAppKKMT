using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace PrintersApp
{
    public class ContextDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");
            //optionsBuilder.UseSqlite("Filename=Printer.db");
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
            public string Name { get; set; }
            public int InventoryNumber { get; set; }
            public VarLocation Location { get; set; }
        }

        public class PrinterInRoom
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Room { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public Printer PrinterObject { get; set; }
        }

        public class Cartridge
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public int StockCount { get; set; }
            public VarLocation Location { get; set; }
        }

        public class Comming
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public Cartridge CartridgeObject { get; set; }
            public int Count { get; set; }
            public DateTime CommingDate { get; set; }
        }

        public class Shipment
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public Printer PrinterObject { get; set; }
            public string Room { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public Cartridge CartridgeObject { get; set; }
            public DateTime ShipmentDate { get; set;}
        }

        public class PrinterCartridge
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [ForeignKey(nameof(Printer))]
            public int PrinterId { get; set; }
            public Printer Printer { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public Cartridge Cartridge { get; set; }
        }
    }
}
