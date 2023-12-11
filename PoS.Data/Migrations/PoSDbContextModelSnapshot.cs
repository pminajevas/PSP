﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoS.Data.Context;

#nullable disable

namespace PoS.Data.Migrations
{
    [DbContext(typeof(PoSDbContext))]
    partial class PoSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PoS.Data.Appointment", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Duration")
                        .HasColumnType("float");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReservationTime")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ServiceId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PoS.Data.Business", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("PoS.Data.Coupon", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Amount")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Validity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("PoS.Data.Customer", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("LoyaltyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Points")
                        .HasColumnType("float");

                    b.HasKey("Id", "BusinessId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PoS.Data.Discount", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiscountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiscountPercentage")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("PoS.Data.Item", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PoS.Data.LoyaltyProgram", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("PointsPerPurchase")
                        .HasColumnType("float");

                    b.Property<string>("RedemptionRules")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("SpecialBenefits")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("LoyaltyPrograms");
                });

            modelBuilder.Entity("PoS.Data.Order", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TaxId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Tip")
                        .HasColumnType("float");

                    b.Property<double?>("TotalAmount")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PoS.Data.OrderItem", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Quantity")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("Subtotal")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double?>("UnitPrice")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("PoS.Data.Payment", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Amount")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<Guid?>("OrderId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PaymentDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PaymentMethodId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PoS.Data.PaymentMethod", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MethodDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("PoS.Data.Role", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PoS.Data.Service", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DiscountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Duration")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("StaffId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("PoS.Data.Staff", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RolesAsString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers");
                });

            modelBuilder.Entity("PoS.Data.Tax", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("TaxDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TaxName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double?>("TaxPercentage")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<DateTime?>("ValidFrom")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidUntil")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("PoS.Data.UserLogin", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LoginDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("LogoutDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}
