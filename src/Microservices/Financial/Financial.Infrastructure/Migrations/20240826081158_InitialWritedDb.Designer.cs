﻿// <auto-generated />
using System;
using Financial.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Financial.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20240826081158_InitialWritedDb")]
    partial class InitialWritedDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Financial.Domain.Entities.Expence", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SiteId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Expences");
                });

            modelBuilder.Entity("Financial.Domain.Entities.ExpenceItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ExpenceId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastPaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UnitId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpenceId");

                    b.ToTable("ExpenceItems");
                });

            modelBuilder.Entity("Financial.Domain.Entities.Payment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ExpenceItemId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Financial.Domain.Entities.ExpenceItem", b =>
                {
                    b.HasOne("Financial.Domain.Entities.Expence", null)
                        .WithMany("ExpenceItems")
                        .HasForeignKey("ExpenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Financial.Domain.Entities.Expence", b =>
                {
                    b.Navigation("ExpenceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
