﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PrintersApp;

#nullable disable

namespace PrintersApp.Migrations
{
    [DbContext(typeof(ContextDataBase))]
    [Migration("20240610121126_klema")]
    partial class klema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PrinterApp")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PrintersApp.ContextDataBase+Cartridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StockCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Cartridges", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Comming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CartridgeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CommingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartridgeId");

                    b.ToTable("Commings", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Printer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("InventoryNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Printers", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+PrinterCartridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CartridgeId")
                        .HasColumnType("integer");

                    b.Property<int>("PrinterId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartridgeId");

                    b.HasIndex("PrinterId");

                    b.ToTable("PrinterCartridges", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+PrinterInRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PrinterId")
                        .HasColumnType("integer");

                    b.Property<string>("Room")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PrinterId");

                    b.ToTable("PrinterInRooms", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Projector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("InvenrotyNumber")
                        .HasColumnType("integer");

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.Property<string>("MileageHours")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projectors", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+ProjectorsInRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectorId")
                        .HasColumnType("integer");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectorId");

                    b.ToTable("ProjectorsInRooms", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+RepairSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateRepair")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DescriptionRepair")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WorkStationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorkStationId");

                    b.ToTable("RepairSessions", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CartridgeId")
                        .HasColumnType("integer");

                    b.Property<int>("PrinterId")
                        .HasColumnType("integer");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ShipmentDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CartridgeId");

                    b.HasIndex("PrinterId");

                    b.ToTable("Shipments", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+WorkStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CPU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GPU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Keyboard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.Property<string>("Monitor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Motherboard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Mouse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RAMName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RAMVolume")
                        .HasColumnType("integer");

                    b.Property<string>("ROMHddName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ROMHddVolume")
                        .HasColumnType("integer");

                    b.Property<string>("ROMSsdName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ROMSsdVolume")
                        .HasColumnType("integer");

                    b.Property<string>("UPS")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkStations", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+WorkStationsInRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WorkStationId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkStationStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WorkStationId");

                    b.ToTable("WorkStationsInRooms", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Comming", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Cartridge", "CartridgeObject")
                        .WithMany()
                        .HasForeignKey("CartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeObject");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+PrinterCartridge", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Cartridge", "Cartridge")
                        .WithMany()
                        .HasForeignKey("CartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintersApp.ContextDataBase+Printer", "Printer")
                        .WithMany()
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cartridge");

                    b.Navigation("Printer");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+PrinterInRoom", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Printer", "PrinterObject")
                        .WithMany()
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrinterObject");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+ProjectorsInRoom", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Projector", "ProjectorObject")
                        .WithMany()
                        .HasForeignKey("ProjectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectorObject");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+RepairSession", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+WorkStation", "WorkStationObject")
                        .WithMany()
                        .HasForeignKey("WorkStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkStationObject");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Shipment", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Cartridge", "CartridgeObject")
                        .WithMany()
                        .HasForeignKey("CartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintersApp.ContextDataBase+Printer", "PrinterObject")
                        .WithMany()
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeObject");

                    b.Navigation("PrinterObject");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+WorkStationsInRoom", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+WorkStation", "WorkStationObject")
                        .WithMany()
                        .HasForeignKey("WorkStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkStationObject");
                });
#pragma warning restore 612, 618
        }
    }
}