﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pss.Reference.Accessors.Products;

#nullable disable

namespace Pss.Reference.Accessors.Products.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20221008003509_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Pss.Reference.Accessors.Products.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int>("CurrentQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProductJson")
                        .IsRequired()
                        .HasMaxLength(8192)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ReorderQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("SellPrice")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0.0m);

                    b.Property<string>("StockKeepingUnit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 90,
                            CurrentQuantity = 52,
                            Description = "Description-26034",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-26034",
                            Name = "Name-26034",
                            ProductJson = "{\"productId\":90,\"productType\":1,\"manufacturer\":\"X-Manufacturer-26034\",\"stockKeepingUnit\":\"X-SKU-26034\",\"name\":\"Name-26034\",\"description\":\"Description-26034\",\"sellPrice\":247.0,\"currentQuantity\":52,\"reorderQuantity\":11,\"isDeleted\":true}",
                            ProductType = "Commodity",
                            ReorderQuantity = 11,
                            SellPrice = 247m,
                            StockKeepingUnit = "X-SKU-26034"
                        },
                        new
                        {
                            ProductId = 32,
                            CurrentQuantity = 202,
                            Description = "Description-35004",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-35004",
                            Name = "Name-35004",
                            ProductJson = "{\"productId\":32,\"productType\":1,\"manufacturer\":\"X-Manufacturer-35004\",\"stockKeepingUnit\":\"X-SKU-87246\",\"name\":\"Name-35004\",\"description\":\"Description-35004\",\"sellPrice\":7.0,\"currentQuantity\":202,\"reorderQuantity\":65,\"isDeleted\":false}",
                            ProductType = "Commodity",
                            ReorderQuantity = 65,
                            SellPrice = 7m,
                            StockKeepingUnit = "X-SKU-87246"
                        },
                        new
                        {
                            ProductId = 161,
                            CurrentQuantity = 95,
                            Description = "Description-703",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-52944",
                            Name = "Name-703",
                            ProductJson = "{\"productId\":161,\"productType\":1,\"manufacturer\":\"X-Manufacturer-52944\",\"stockKeepingUnit\":\"X-SKU-52944\",\"name\":\"Name-703\",\"description\":\"Description-703\",\"sellPrice\":43.0,\"currentQuantity\":95,\"reorderQuantity\":44,\"isDeleted\":true}",
                            ProductType = "Commodity",
                            ReorderQuantity = 44,
                            SellPrice = 43m,
                            StockKeepingUnit = "X-SKU-52944"
                        },
                        new
                        {
                            ProductId = 60,
                            CurrentQuantity = 24,
                            Description = "Description-61914",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-61914",
                            Name = "Name-61914",
                            ProductJson = "{\"productId\":60,\"productType\":1,\"manufacturer\":\"X-Manufacturer-61914\",\"stockKeepingUnit\":\"X-SKU-61914\",\"name\":\"Name-61914\",\"description\":\"Description-61914\",\"sellPrice\":240.0,\"currentQuantity\":24,\"reorderQuantity\":107,\"isDeleted\":false}",
                            ProductType = "Commodity",
                            ReorderQuantity = 107,
                            SellPrice = 240m,
                            StockKeepingUnit = "X-SKU-61914"
                        },
                        new
                        {
                            ProductId = 249,
                            CurrentQuantity = 192,
                            Description = "Description-70884",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-70884",
                            Name = "Name-70884",
                            ProductJson = "{\"productId\":249,\"productType\":1,\"manufacturer\":\"X-Manufacturer-70884\",\"stockKeepingUnit\":\"X-SKU-70884\",\"name\":\"Name-70884\",\"description\":\"Description-70884\",\"sellPrice\":98.0,\"currentQuantity\":192,\"reorderQuantity\":57,\"isDeleted\":true}",
                            ProductType = "Commodity",
                            ReorderQuantity = 57,
                            SellPrice = 98m,
                            StockKeepingUnit = "X-SKU-70884"
                        },
                        new
                        {
                            ProductId = 182,
                            CurrentQuantity = 231,
                            Description = "Description-27612",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-27612",
                            Name = "Name-27612",
                            ProductJson = "{\"productId\":182,\"productType\":1,\"manufacturer\":\"X-Manufacturer-27612\",\"stockKeepingUnit\":\"X-SKU-79854\",\"name\":\"Name-27612\",\"description\":\"Description-27612\",\"sellPrice\":115.0,\"currentQuantity\":231,\"reorderQuantity\":224,\"isDeleted\":false}",
                            ProductType = "Commodity",
                            ReorderQuantity = 224,
                            SellPrice = 115m,
                            StockKeepingUnit = "X-SKU-79854"
                        },
                        new
                        {
                            ProductId = 101,
                            CurrentQuantity = 232,
                            Description = "Description-88824",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-88824",
                            Name = "Name-88824",
                            ProductJson = "{\"productId\":101,\"productType\":1,\"manufacturer\":\"X-Manufacturer-88824\",\"stockKeepingUnit\":\"X-SKU-88824\",\"name\":\"Name-88824\",\"description\":\"Description-88824\",\"sellPrice\":229.0,\"currentQuantity\":232,\"reorderQuantity\":211,\"isDeleted\":true}",
                            ProductType = "Commodity",
                            ReorderQuantity = 211,
                            SellPrice = 229m,
                            StockKeepingUnit = "X-SKU-88824"
                        },
                        new
                        {
                            ProductId = 214,
                            CurrentQuantity = 188,
                            Description = "Description-97794",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-97794",
                            Name = "Name-97794",
                            ProductJson = "{\"productId\":214,\"productType\":1,\"manufacturer\":\"X-Manufacturer-97794\",\"stockKeepingUnit\":\"X-SKU-97794\",\"name\":\"Name-97794\",\"description\":\"Description-97794\",\"sellPrice\":114.0,\"currentQuantity\":188,\"reorderQuantity\":30,\"isDeleted\":false}",
                            ProductType = "Commodity",
                            ReorderQuantity = 30,
                            SellPrice = 114m,
                            StockKeepingUnit = "X-SKU-97794"
                        },
                        new
                        {
                            ProductId = 176,
                            CurrentQuantity = 145,
                            Description = "Description-6766",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-6766",
                            Name = "Name-6766",
                            ProductJson = "{\"productId\":176,\"productType\":1,\"manufacturer\":\"X-Manufacturer-6766\",\"stockKeepingUnit\":\"X-SKU-6766\",\"name\":\"Name-6766\",\"description\":\"Description-6766\",\"sellPrice\":97.0,\"currentQuantity\":145,\"reorderQuantity\":242,\"isDeleted\":true}",
                            ProductType = "Commodity",
                            ReorderQuantity = 242,
                            SellPrice = 97m,
                            StockKeepingUnit = "X-SKU-6766"
                        },
                        new
                        {
                            ProductId = 118,
                            CurrentQuantity = 140,
                            Description = "Description-15736",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-15736",
                            Name = "Name-63492",
                            ProductJson = "{\"productId\":118,\"productType\":1,\"manufacturer\":\"X-Manufacturer-15736\",\"stockKeepingUnit\":\"X-SKU-15736\",\"name\":\"Name-63492\",\"description\":\"Description-15736\",\"sellPrice\":237.0,\"currentQuantity\":140,\"reorderQuantity\":167,\"isDeleted\":false}",
                            ProductType = "Commodity",
                            ReorderQuantity = 167,
                            SellPrice = 237m,
                            StockKeepingUnit = "X-SKU-15736"
                        },
                        new
                        {
                            ProductId = 96,
                            CurrentQuantity = 190,
                            Description = "Description-24706",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-24706",
                            Name = "Name-24706",
                            ProductJson = "{\"vendor\":\"Vendor-24706\",\"productId\":96,\"productType\":2,\"manufacturer\":\"X-Manufacturer-24706\",\"stockKeepingUnit\":\"X-SKU-24706\",\"name\":\"Name-24706\",\"description\":\"Description-24706\",\"sellPrice\":169.0,\"currentQuantity\":190,\"reorderQuantity\":148,\"isDeleted\":true}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 148,
                            SellPrice = 169m,
                            StockKeepingUnit = "X-SKU-24706"
                        },
                        new
                        {
                            ProductId = 117,
                            CurrentQuantity = 215,
                            Description = "Description-33676",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-33676",
                            Name = "Name-33676",
                            ProductJson = "{\"vendor\":\"Vendor-33676\",\"productId\":117,\"productType\":2,\"manufacturer\":\"X-Manufacturer-33676\",\"stockKeepingUnit\":\"X-SKU-33676\",\"name\":\"Name-33676\",\"description\":\"Description-33676\",\"sellPrice\":235.0,\"currentQuantity\":215,\"reorderQuantity\":186,\"isDeleted\":false}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 186,
                            SellPrice = 235m,
                            StockKeepingUnit = "X-SKU-33676"
                        },
                        new
                        {
                            ProductId = 62,
                            CurrentQuantity = 9,
                            Description = "Description-42646",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-42646",
                            Name = "Name-42646",
                            ProductJson = "{\"vendor\":\"Vendor-42646\",\"productId\":62,\"productType\":2,\"manufacturer\":\"X-Manufacturer-42646\",\"stockKeepingUnit\":\"X-SKU-42646\",\"name\":\"Name-42646\",\"description\":\"Description-42646\",\"sellPrice\":172.0,\"currentQuantity\":9,\"reorderQuantity\":15,\"isDeleted\":true}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 15,
                            SellPrice = 172m,
                            StockKeepingUnit = "X-SKU-42646"
                        },
                        new
                        {
                            ProductId = 158,
                            CurrentQuantity = 184,
                            Description = "Description-51616",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-51616",
                            Name = "Name-51616",
                            ProductJson = "{\"vendor\":\"Vendor-51616\",\"productId\":158,\"productType\":2,\"manufacturer\":\"X-Manufacturer-51616\",\"stockKeepingUnit\":\"X-SKU-51616\",\"name\":\"Name-51616\",\"description\":\"Description-51616\",\"sellPrice\":46.0,\"currentQuantity\":184,\"reorderQuantity\":227,\"isDeleted\":false}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 227,
                            SellPrice = 46m,
                            StockKeepingUnit = "X-SKU-51616"
                        },
                        new
                        {
                            ProductId = 33,
                            CurrentQuantity = 198,
                            Description = "Description-12829",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-12829",
                            Name = "Name-12829",
                            ProductJson = "{\"vendor\":\"Vendor-12829\",\"productId\":33,\"productType\":2,\"manufacturer\":\"X-Manufacturer-12829\",\"stockKeepingUnit\":\"X-SKU-12829\",\"name\":\"Name-12829\",\"description\":\"Description-12829\",\"sellPrice\":254.0,\"currentQuantity\":198,\"reorderQuantity\":74,\"isDeleted\":true}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 74,
                            SellPrice = 254m,
                            StockKeepingUnit = "X-SKU-12829"
                        },
                        new
                        {
                            ProductId = 1,
                            CurrentQuantity = 133,
                            Description = "Description-21799",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-21799",
                            Name = "Name-21799",
                            ProductJson = "{\"vendor\":\"Vendor-21799\",\"productId\":1,\"productType\":2,\"manufacturer\":\"X-Manufacturer-21799\",\"stockKeepingUnit\":\"X-SKU-21799\",\"name\":\"Name-21799\",\"description\":\"Description-21799\",\"sellPrice\":70.0,\"currentQuantity\":133,\"reorderQuantity\":193,\"isDeleted\":false}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 193,
                            SellPrice = 70m,
                            StockKeepingUnit = "X-SKU-21799"
                        },
                        new
                        {
                            ProductId = 206,
                            CurrentQuantity = 119,
                            Description = "Description-35254",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-35254",
                            Name = "Name-35254",
                            ProductJson = "{\"vendor\":\"Vendor-83010\",\"productId\":206,\"productType\":2,\"manufacturer\":\"X-Manufacturer-35254\",\"stockKeepingUnit\":\"X-SKU-35254\",\"name\":\"Name-35254\",\"description\":\"Description-35254\",\"sellPrice\":31.0,\"currentQuantity\":119,\"reorderQuantity\":55,\"isDeleted\":true}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 55,
                            SellPrice = 31m,
                            StockKeepingUnit = "X-SKU-35254"
                        },
                        new
                        {
                            ProductId = 183,
                            CurrentQuantity = 78,
                            Description = "Description-44224",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-96465",
                            Name = "Name-44224",
                            ProductJson = "{\"vendor\":\"Vendor-44224\",\"productId\":183,\"productType\":2,\"manufacturer\":\"X-Manufacturer-96465\",\"stockKeepingUnit\":\"X-SKU-96465\",\"name\":\"Name-44224\",\"description\":\"Description-44224\",\"sellPrice\":209.0,\"currentQuantity\":78,\"reorderQuantity\":59,\"isDeleted\":false}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 59,
                            SellPrice = 209m,
                            StockKeepingUnit = "X-SKU-96465"
                        },
                        new
                        {
                            ProductId = 171,
                            CurrentQuantity = 250,
                            Description = "Description-5437",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-5437",
                            Name = "Name-5437",
                            ProductJson = "{\"vendor\":\"Vendor-5437\",\"productId\":171,\"productType\":2,\"manufacturer\":\"X-Manufacturer-5437\",\"stockKeepingUnit\":\"X-SKU-5437\",\"name\":\"Name-5437\",\"description\":\"Description-5437\",\"sellPrice\":72.0,\"currentQuantity\":250,\"reorderQuantity\":170,\"isDeleted\":true}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 170,
                            SellPrice = 72m,
                            StockKeepingUnit = "X-SKU-5437"
                        },
                        new
                        {
                            ProductId = 82,
                            CurrentQuantity = 168,
                            Description = "Description-66649",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-18892",
                            Name = "Name-66649",
                            ProductJson = "{\"vendor\":\"Vendor-66649\",\"productId\":82,\"productType\":2,\"manufacturer\":\"X-Manufacturer-18892\",\"stockKeepingUnit\":\"X-SKU-18892\",\"name\":\"Name-66649\",\"description\":\"Description-66649\",\"sellPrice\":201.0,\"currentQuantity\":168,\"reorderQuantity\":128,\"isDeleted\":false}",
                            ProductType = "SalonProduct",
                            ReorderQuantity = 128,
                            SellPrice = 201m,
                            StockKeepingUnit = "X-SKU-18892"
                        },
                        new
                        {
                            ProductId = 50,
                            CurrentQuantity = 185,
                            Description = "Description-36832",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-36832",
                            Name = "Name-36832",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-84589\",\"make\":\"Make-84589\",\"model\":\"Model-84589\",\"manufactureYear\":225,\"licenseNumber\":\"L-3683\",\"productId\":50,\"productType\":3,\"manufacturer\":\"X-Manufacturer-36832\",\"stockKeepingUnit\":\"X-SKU-36832\",\"name\":\"Name-36832\",\"description\":\"Description-36832\",\"sellPrice\":13.0,\"currentQuantity\":185,\"reorderQuantity\":146,\"isDeleted\":true}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 146,
                            SellPrice = 13m,
                            StockKeepingUnit = "X-SKU-36832"
                        },
                        new
                        {
                            ProductId = 6,
                            CurrentQuantity = 143,
                            Description = "Description-50287",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-50287",
                            Name = "Name-50287",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-98043\",\"make\":\"Make-50287\",\"model\":\"Model-50287\",\"manufactureYear\":49,\"licenseNumber\":\"L-5028\",\"productId\":6,\"productType\":3,\"manufacturer\":\"X-Manufacturer-50287\",\"stockKeepingUnit\":\"X-SKU-50287\",\"name\":\"Name-50287\",\"description\":\"Description-50287\",\"sellPrice\":142.0,\"currentQuantity\":143,\"reorderQuantity\":40,\"isDeleted\":false}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 40,
                            SellPrice = 142m,
                            StockKeepingUnit = "X-SKU-50287"
                        },
                        new
                        {
                            ProductId = 220,
                            CurrentQuantity = 210,
                            Description = "Description-11500",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-11500",
                            Name = "Name-11500",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-11500\",\"make\":\"Make-11500\",\"model\":\"Model-11500\",\"manufactureYear\":26,\"licenseNumber\":\"L-1150\",\"productId\":220,\"productType\":3,\"manufacturer\":\"X-Manufacturer-11500\",\"stockKeepingUnit\":\"X-SKU-11500\",\"name\":\"Name-11500\",\"description\":\"Description-11500\",\"sellPrice\":8.0,\"currentQuantity\":210,\"reorderQuantity\":103,\"isDeleted\":true}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 103,
                            SellPrice = 8m,
                            StockKeepingUnit = "X-SKU-11500"
                        },
                        new
                        {
                            ProductId = 39,
                            CurrentQuantity = 126,
                            Description = "Description-77197",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-77197",
                            Name = "Name-77197",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-77197\",\"make\":\"Make-77197\",\"model\":\"Model-77197\",\"manufactureYear\":153,\"licenseNumber\":\"L-7719\",\"productId\":39,\"productType\":3,\"manufacturer\":\"X-Manufacturer-77197\",\"stockKeepingUnit\":\"X-SKU-77197\",\"name\":\"Name-77197\",\"description\":\"Description-77197\",\"sellPrice\":94.0,\"currentQuantity\":126,\"reorderQuantity\":212,\"isDeleted\":false}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 212,
                            SellPrice = 94m,
                            StockKeepingUnit = "X-SKU-77197"
                        },
                        new
                        {
                            ProductId = 77,
                            CurrentQuantity = 151,
                            Description = "Description-90652",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-90652",
                            Name = "Name-90652",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-38410\",\"make\":\"Make-38410\",\"model\":\"Model-90652\",\"manufactureYear\":34,\"licenseNumber\":\"L-9064\",\"productId\":77,\"productType\":3,\"manufacturer\":\"X-Manufacturer-90652\",\"stockKeepingUnit\":\"X-SKU-90652\",\"name\":\"Name-90652\",\"description\":\"Description-90652\",\"sellPrice\":64.0,\"currentQuantity\":151,\"reorderQuantity\":61,\"isDeleted\":true}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 61,
                            SellPrice = 64m,
                            StockKeepingUnit = "X-SKU-90652"
                        },
                        new
                        {
                            ProductId = 141,
                            CurrentQuantity = 122,
                            Description = "Description-4109",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-4109",
                            Name = "Name-51865",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-51865\",\"make\":\"Make-51865\",\"model\":\"Model-51865\",\"manufactureYear\":253,\"licenseNumber\":\"L-5186\",\"productId\":141,\"productType\":3,\"manufacturer\":\"X-Manufacturer-4109\",\"stockKeepingUnit\":\"X-SKU-4109\",\"name\":\"Name-51865\",\"description\":\"Description-4109\",\"sellPrice\":164.0,\"currentQuantity\":122,\"reorderQuantity\":144,\"isDeleted\":false}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 144,
                            SellPrice = 164m,
                            StockKeepingUnit = "X-SKU-4109"
                        },
                        new
                        {
                            ProductId = 106,
                            CurrentQuantity = 200,
                            Description = "Description-17564",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-17564",
                            Name = "Name-17564",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-17564\",\"make\":\"Make-17564\",\"model\":\"Model-17564\",\"manufactureYear\":73,\"licenseNumber\":\"L-1756\",\"productId\":106,\"productType\":3,\"manufacturer\":\"X-Manufacturer-17564\",\"stockKeepingUnit\":\"X-SKU-17564\",\"name\":\"Name-17564\",\"description\":\"Description-17564\",\"sellPrice\":159.0,\"currentQuantity\":200,\"reorderQuantity\":208,\"isDeleted\":true}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 208,
                            SellPrice = 159m,
                            StockKeepingUnit = "X-SKU-17564"
                        },
                        new
                        {
                            ProductId = 236,
                            CurrentQuantity = 54,
                            Description = "Description-31018",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-31018",
                            Name = "Name-31018",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-78775\",\"make\":\"Make-78775\",\"model\":\"Model-78775\",\"manufactureYear\":203,\"licenseNumber\":\"L-7877\",\"productId\":236,\"productType\":3,\"manufacturer\":\"X-Manufacturer-31018\",\"stockKeepingUnit\":\"X-SKU-31018\",\"name\":\"Name-31018\",\"description\":\"Description-31018\",\"sellPrice\":243.0,\"currentQuantity\":54,\"reorderQuantity\":22,\"isDeleted\":false}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 22,
                            SellPrice = 243m,
                            StockKeepingUnit = "X-SKU-31018"
                        },
                        new
                        {
                            ProductId = 18,
                            CurrentQuantity = 197,
                            Description = "Description-53443",
                            IsDeleted = true,
                            Manufacturer = "X-Manufacturer-53443",
                            Name = "Name-53443",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-53443\",\"make\":\"Make-53443\",\"model\":\"Model-53443\",\"manufactureYear\":89,\"licenseNumber\":\"L-5344\",\"productId\":18,\"productType\":3,\"manufacturer\":\"X-Manufacturer-53443\",\"stockKeepingUnit\":\"X-SKU-5687\",\"name\":\"Name-53443\",\"description\":\"Description-53443\",\"sellPrice\":233.0,\"currentQuantity\":197,\"reorderQuantity\":41,\"isDeleted\":true}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 41,
                            SellPrice = 233m,
                            StockKeepingUnit = "X-SKU-5687"
                        },
                        new
                        {
                            ProductId = 6706,
                            CurrentQuantity = 8805,
                            Description = "Description-71383",
                            IsDeleted = false,
                            Manufacturer = "X-Manufacturer-71383",
                            Name = "Name-71383",
                            ProductJson = "{\"vehicleIdentificationNumber\":\"VIN-71383\",\"make\":\"Make-71383\",\"model\":\"Model-71383\",\"manufactureYear\":14706,\"licenseNumber\":\"L-7137\",\"productId\":6706,\"productType\":3,\"manufacturer\":\"X-Manufacturer-71383\",\"stockKeepingUnit\":\"X-SKU-71383\",\"name\":\"Name-71383\",\"description\":\"Description-71383\",\"sellPrice\":5278.0,\"currentQuantity\":8805,\"reorderQuantity\":7364,\"isDeleted\":false}",
                            ProductType = "Vehicle",
                            ReorderQuantity = 7364,
                            SellPrice = 5278m,
                            StockKeepingUnit = "X-SKU-71383"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
