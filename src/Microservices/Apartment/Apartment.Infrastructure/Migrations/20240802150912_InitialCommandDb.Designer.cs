﻿// <auto-generated />
using System;
using Apartment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Apartment.Infrastructure.Migrations
{
    [DbContext(typeof(CommandDbContext))]
    [Migration("20240802150912_InitialCommandDb")]
    partial class InitialCommandDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Apartment.Domain.Entities.Block", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SiteId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalUnits")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Site", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ManagerId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Unit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("BlockId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasCar")
                        .HasColumnType("boolean");

                    b.Property<string>("ResidentId")
                        .HasColumnType("text");

                    b.Property<int>("UnitNo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Visit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("HasCar")
                        .HasColumnType("boolean");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("text");

                    b.Property<string>("UnitId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Block", b =>
                {
                    b.HasOne("Apartment.Domain.Entities.Site", null)
                        .WithMany("Blocks")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Unit", b =>
                {
                    b.HasOne("Apartment.Domain.Entities.Block", null)
                        .WithMany("Units")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Block", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("Apartment.Domain.Entities.Site", b =>
                {
                    b.Navigation("Blocks");
                });
#pragma warning restore 612, 618
        }
    }
}
