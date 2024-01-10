﻿// <auto-generated />
using System;
using Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repository.Migrations
{
    [DbContext(typeof(TelegramBotDbContext))]
    [Migration("20240104145835_Added_NewColumns_In_UserActivity_Table")]
    partial class Added_NewColumns_In_UserActivity_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Repository.Entity.Bank", b =>
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

            modelBuilder.Entity("Repository.Entity.BankLocation", b =>
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

            modelBuilder.Entity("Repository.Entity.BotHistory", b =>
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

            modelBuilder.Entity("Repository.Entity.ChatDetail", b =>
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

            modelBuilder.Entity("Repository.Entity.Currency", b =>
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

            modelBuilder.Entity("Repository.Entity.RateModel", b =>
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

            modelBuilder.Entity("Repository.Entity.UserActivityHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserExternalBi")
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

                    b.Property<long>("UserExternalId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UsersActivityHistories");
                });

            modelBuilder.Entity("Repository.Entity.BankLocation", b =>
                {
                    b.HasOne("Repository.Entity.Bank", "Bank")
                        .WithMany("Locations")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Repository.Entity.ChatDetail", b =>
                {
                    b.HasOne("Repository.Entity.UserActivityHistory", "UsersActivityHistory")
                        .WithMany("ChatDetails")
                        .HasForeignKey("UserActivityHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsersActivityHistory");
                });

            modelBuilder.Entity("Repository.Entity.RateModel", b =>
                {
                    b.HasOne("Repository.Entity.Bank", "Bank")
                        .WithMany("Rates")
                        .HasForeignKey("BankId")
                        .HasConstraintName("FK_Rates_Banks")
                        .IsRequired();

                    b.HasOne("Repository.Entity.Currency", "FromCurrencyNavigation")
                        .WithMany("RateFromCurrencyNavigations")
                        .HasForeignKey("FromCurrency")
                        .HasConstraintName("FK_Rates_Currencies")
                        .IsRequired();

                    b.HasOne("Repository.Entity.Currency", "ToCurrencyNavigation")
                        .WithMany("RateToCurrencyNavigations")
                        .HasForeignKey("ToCurrency")
                        .HasConstraintName("FK_Rates_Currencies1")
                        .IsRequired();

                    b.Navigation("Bank");

                    b.Navigation("FromCurrencyNavigation");

                    b.Navigation("ToCurrencyNavigation");
                });

            modelBuilder.Entity("Repository.Entity.Bank", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Rates");
                });

            modelBuilder.Entity("Repository.Entity.Currency", b =>
                {
                    b.Navigation("RateFromCurrencyNavigations");

                    b.Navigation("RateToCurrencyNavigations");
                });

            modelBuilder.Entity("Repository.Entity.UserActivityHistory", b =>
                {
                    b.Navigation("ChatDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
