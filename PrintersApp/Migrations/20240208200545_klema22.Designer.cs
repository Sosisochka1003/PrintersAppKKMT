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
    [Migration("20240208200545_klema22")]
    partial class klema22
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PrinterApp")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PrintersApp.ContextDataBase+Cartridge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

                    b.Property<int>("CartridgeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CartridgeId");

                    b.ToTable("Printers", "PrinterApp");
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PrinterId");

                    b.ToTable("PrinterInRooms", "PrinterApp");
                });

            modelBuilder.Entity("PrintersApp.ContextDataBase+Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CartridgeId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("Printer")
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

                    b.HasIndex("Printer");

                    b.ToTable("Shipments", "PrinterApp");
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

            modelBuilder.Entity("PrintersApp.ContextDataBase+Printer", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Cartridge", "CartridgeObject")
                        .WithMany()
                        .HasForeignKey("CartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeObject");
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

            modelBuilder.Entity("PrintersApp.ContextDataBase+Shipment", b =>
                {
                    b.HasOne("PrintersApp.ContextDataBase+Cartridge", "CartridgeObject")
                        .WithMany()
                        .HasForeignKey("CartridgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrintersApp.ContextDataBase+Printer", "PrinterObject")
                        .WithMany()
                        .HasForeignKey("Printer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeObject");

                    b.Navigation("PrinterObject");
                });
#pragma warning restore 612, 618
        }
    }
}
