﻿// <auto-generated />
using System;
using Freddy.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Freddy.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Freddy.Persistence.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8e704345-26bc-4091-a9cc-0ca052c03556"),
                            Email = "ipj@humail.hu",
                            Name = "Ilosfai-Pataki Júlia",
                            Phone = "+36 900 800 4445"
                        },
                        new
                        {
                            Id = new Guid("2f4172f9-8537-4059-8567-31d5e36029a9"),
                            Email = "bujdosoreka@humail.hu",
                            Name = "Bujdosó Réka",
                            Phone = "+36 123 90 8999"
                        });
                });

            modelBuilder.Entity("Freddy.Persistence.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464"),
                            Code = "WRUP1LC001, P4",
                            Name = "Fáradt rózsaszín pamut nadrág- csípő",
                            Size = "M"
                        },
                        new
                        {
                            Id = new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f"),
                            Code = "WRUP1LC001, N",
                            Name = "Fekete pamut nadrág- csípő",
                            Size = "XL"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
