﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLNH_Client.Data;

#nullable disable

namespace QLNH_Client.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230620090422_Add ItemImage model")]
    partial class AddItemImagemodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QLNH_Client.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("QLNH_Client.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("QLNH_Client.Models.ItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Base64")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("QLNH_Client.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("QLNH_Client.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("QLNH_Client.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OffDuty")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QLNH_Client.Models.Category", b =>
                {
                    b.HasOne("QLNH_Client.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("QLNH_Client.Models.Restaurant", "Restaurant")
                        .WithMany("Categories")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Parent");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("QLNH_Client.Models.Item", b =>
                {
                    b.HasOne("QLNH_Client.Models.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId");

                    b.HasOne("QLNH_Client.Models.Restaurant", "Restaurant")
                        .WithMany("Items")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Category");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("QLNH_Client.Models.ItemImage", b =>
                {
                    b.HasOne("QLNH_Client.Models.Item", "Item")
                        .WithMany("ItemImages")
                        .HasForeignKey("ItemId");

                    b.HasOne("QLNH_Client.Models.Restaurant", "Restaurant")
                        .WithMany("ItemImages")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Item");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("QLNH_Client.Models.Restaurant", b =>
                {
                    b.HasOne("QLNH_Client.Models.User", "CreatedUser")
                        .WithMany("RestaurantCreator")
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("QLNH_Client.Models.User", "UpdatedUser")
                        .WithMany("RestaurantUpdator")
                        .HasForeignKey("UpdatedUserId");

                    b.Navigation("CreatedUser");

                    b.Navigation("UpdatedUser");
                });

            modelBuilder.Entity("QLNH_Client.Models.User", b =>
                {
                    b.HasOne("QLNH_Client.Models.Restaurant", "Restaurant")
                        .WithMany("Users")
                        .HasForeignKey("RestaurantId");

                    b.HasOne("QLNH_Client.Models.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("RoleId")
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("QLNH_Client.Models.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("QLNH_Client.Models.Item", b =>
                {
                    b.Navigation("ItemImages");
                });

            modelBuilder.Entity("QLNH_Client.Models.Restaurant", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("ItemImages");

                    b.Navigation("Items");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("QLNH_Client.Models.Role", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("QLNH_Client.Models.User", b =>
                {
                    b.Navigation("RestaurantCreator");

                    b.Navigation("RestaurantUpdator");
                });
#pragma warning restore 612, 618
        }
    }
}
