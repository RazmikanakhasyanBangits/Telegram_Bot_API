﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(TelegramBotDbContext))]
    [Migration("20240105175746_Added_Status_And_Role_For_User")]
    partial class Added_Status_And_Role_For_User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BankUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("BankURL");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("DataAccess.Models.BankLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("DataAccess.Models.BotHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommandText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BotHistory");
                });

            modelBuilder.Entity("DataAccess.Models.ChatDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MessageExternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("Response")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserActivityHistoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("UserActivityHistoryId");

                    b.ToTable("ChatDetails");
                });

            modelBuilder.Entity("DataAccess.Models.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code")
                        .HasName("PK_Currencies_1");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("DataAccess.Models.RateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankId")
                        .HasColumnType("int");

                    b.Property<decimal>("BuyValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FromCurrency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("Iteration")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SellValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ToCurrency")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.HasIndex(new[] { "FromCurrency" }, "IX_Rates_FromCurrency");

                    b.HasIndex(new[] { "ToCurrency" }, "IX_Rates_ToCurrency");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("DataAccess.Models.UserActivityHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<short>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)2);

                    b.Property<short>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1);

                    b.Property<long>("UserExternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("UsersActivityHistories");
                });

            modelBuilder.Entity("DataAccess.Models.UserRole", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            CreationDate = new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(4716),
                            DeletionDate = new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4719),
                            LastUpdatedDate = new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(4718),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = (short)2,
                            CreationDate = new DateTime(2024, 1, 5, 21, 57, 46, 238, DateTimeKind.Local).AddTicks(5096),
                            DeletionDate = new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5098),
                            LastUpdatedDate = new DateTime(2024, 1, 5, 17, 57, 46, 238, DateTimeKind.Utc).AddTicks(5097),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.UserStatus", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            CreationDate = new DateTime(2024, 1, 5, 21, 57, 46, 236, DateTimeKind.Local).AddTicks(6112),
                            DeletionDate = new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6002),
                            LastUpdatedDate = new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6001),
                            Name = "Active"
                        },
                        new
                        {
                            Id = (short)2,
                            CreationDate = new DateTime(2024, 1, 5, 21, 57, 46, 237, DateTimeKind.Local).AddTicks(6912),
                            DeletionDate = new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920),
                            LastUpdatedDate = new DateTime(2024, 1, 5, 17, 57, 46, 237, DateTimeKind.Utc).AddTicks(6920),
                            Name = "Blocked"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.BankLocation", b =>
                {
                    b.HasOne("DataAccess.Models.Bank", "Bank")
                        .WithMany("Locations")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("DataAccess.Models.ChatDetail", b =>
                {
                    b.HasOne("DataAccess.Models.UserActivityHistory", "UsersActivityHistory")
                        .WithMany("ChatDetails")
                        .HasForeignKey("UserActivityHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsersActivityHistory");
                });

            modelBuilder.Entity("DataAccess.Models.RateModel", b =>
                {
                    b.HasOne("DataAccess.Models.Bank", "Bank")
                        .WithMany("Rates")
                        .HasForeignKey("BankId")
                        .HasConstraintName("FK_Rates_Banks")
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Currency", "FromCurrencyNavigation")
                        .WithMany("RateFromCurrencyNavigations")
                        .HasForeignKey("FromCurrency")
                        .HasConstraintName("FK_Rates_Currencies")
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Currency", "ToCurrencyNavigation")
                        .WithMany("RateToCurrencyNavigations")
                        .HasForeignKey("ToCurrency")
                        .HasConstraintName("FK_Rates_Currencies1")
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("FromCurrencyNavigation");

                    b.Navigation("ToCurrencyNavigation");
                });

            modelBuilder.Entity("DataAccess.Models.UserActivityHistory", b =>
                {
                    b.HasOne("DataAccess.Models.UserRole", "Role")
                        .WithMany("UserActivities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.UserStatus", "Status")
                        .WithMany("UserActivities")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DataAccess.Models.Bank", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("DataAccess.Models.Currency", b =>
                {
                    b.Navigation("RateFromCurrencyNavigations");

                    b.Navigation("RateToCurrencyNavigations");
                });

            modelBuilder.Entity("DataAccess.Models.UserActivityHistory", b =>
                {
                    b.Navigation("ChatDetails");
                });

            modelBuilder.Entity("DataAccess.Models.UserRole", b =>
                {
                    b.Navigation("UserActivities");
                });

            modelBuilder.Entity("DataAccess.Models.UserStatus", b =>
                {
                    b.Navigation("UserActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
