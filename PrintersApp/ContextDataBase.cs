using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Security.Policy;
using System.Runtime.InteropServices.Marshalling;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintersApp
{
    public class ContextDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123");
            optionsBuilder.UseSqlite("Filename=Printer.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PrinterApp");
        }

        public DbSet<Cartridge> Cartridges { get; set; }
        public DbSet<PrinterInRoom> PrinterInRooms { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<Comming> Commings { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        public class Printer
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            [ForeignKey(nameof(Cartridge))]
            public int CartridgeId { get; set; }
            public Cartridge CartridgeObject { get; set; }
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
            public int Id { get; set; }
        }
    }
}
