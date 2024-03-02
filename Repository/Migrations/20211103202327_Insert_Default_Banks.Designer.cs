﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repository.Migrations
{
    [DbContext(typeof(ExchangeBotDbContext))]
    [Migration("20211103202327_Insert_Default_Banks")]
    partial class Insert_Default_Banks
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

            modelBuilder.Entity("Repository.Entity.Rate", b =>
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

            modelBuilder.Entity("Repository.Entity.Rate", b =>
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
                    b.Navigation("Rates");
                });

            modelBuilder.Entity("Repository.Entity.Currency", b =>
                {
                    b.Navigation("RateFromCurrencyNavigations");

                    b.Navigation("RateToCurrencyNavigations");
                });
#pragma warning restore 612, 618
        }
    }
}
