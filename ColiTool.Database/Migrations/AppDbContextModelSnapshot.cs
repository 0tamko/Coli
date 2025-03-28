﻿// <auto-generated />
using ColiTool.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ColiTool.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("ColiTool.Database.Entities.CanMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CanBusSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilterSettings")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CanMessages");
                });

            modelBuilder.Entity("ColiTool.Database.Entities.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CanBusSpeed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilterSettings")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("ColiTool.Database.Entities.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TestEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
