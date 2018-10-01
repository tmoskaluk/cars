﻿// <auto-generated />
using System;
using Cars.Sales.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cars.Sales.Core.Infrastructure.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    [Migration("20180927140728_OrderCustomerAdded")]
    partial class OrderCustomerAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("sales")
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cars.Sales.Core.Domain.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Cars.Sales.Core.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreationDate");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("money");

                    b.Property<DateTime?>("PlannedDeliveryDate");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<DateTime?>("ReceivedDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Cars.Sales.Core.Domain.Entities.Offer", b =>
                {
                    b.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration", "Configuration", b1 =>
                        {
                            b1.Property<Guid>("OfferId");

                            b1.Property<int>("Color");

                            b1.Property<string>("Model")
                                .HasMaxLength(50);

                            b1.Property<int>("Version");

                            b1.ToTable("Offers","sales");

                            b1.HasOne("Cars.Sales.Core.Domain.Entities.Offer")
                                .WithOne("Configuration")
                                .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration", "OfferId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.Engine", "Engine", b2 =>
                                {
                                    b2.Property<Guid>("CarConfigurationOfferId");

                                    b2.Property<int>("Capacity");

                                    b2.Property<string>("Code")
                                        .IsRequired();

                                    b2.Property<int>("Type");

                                    b2.ToTable("Offers","sales");

                                    b2.HasOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration")
                                        .WithOne("Engine")
                                        .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.Engine", "CarConfigurationOfferId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.Gearbox", "Gearbox", b2 =>
                                {
                                    b2.Property<Guid>("CarConfigurationOfferId");

                                    b2.Property<int>("Gears");

                                    b2.Property<int>("Type");

                                    b2.ToTable("Offers","sales");

                                    b2.HasOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration")
                                        .WithOne("Gearbox")
                                        .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.Gearbox", "CarConfigurationOfferId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("Cars.Sales.Core.Domain.Entities.Order", b =>
                {
                    b.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.Customer", "Customer", b1 =>
                        {
                            b1.Property<int?>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("CustomerId");

                            b1.Property<string>("Name");

                            b1.ToTable("Orders","sales");

                            b1.HasOne("Cars.Sales.Core.Domain.Entities.Order")
                                .WithOne("Customer")
                                .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.Customer", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration", "Configuration", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Color");

                            b1.Property<string>("Model");

                            b1.Property<int>("Version");

                            b1.ToTable("Orders","sales");

                            b1.HasOne("Cars.Sales.Core.Domain.Entities.Order")
                                .WithOne("Configuration")
                                .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.Engine", "Engine", b2 =>
                                {
                                    b2.Property<int>("CarConfigurationOrderId")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("Capacity");

                                    b2.Property<string>("Code");

                                    b2.Property<int>("Type");

                                    b2.ToTable("Orders","sales");

                                    b2.HasOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration")
                                        .WithOne("Engine")
                                        .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.Engine", "CarConfigurationOrderId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("Cars.Sales.Core.Domain.ValueObjects.Gearbox", "Gearbox", b2 =>
                                {
                                    b2.Property<int>("CarConfigurationOrderId")
                                        .ValueGeneratedOnAdd()
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("Gears");

                                    b2.Property<int>("Type");

                                    b2.ToTable("Orders","sales");

                                    b2.HasOne("Cars.Sales.Core.Domain.ValueObjects.CarConfiguration")
                                        .WithOne("Gearbox")
                                        .HasForeignKey("Cars.Sales.Core.Domain.ValueObjects.Gearbox", "CarConfigurationOrderId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
