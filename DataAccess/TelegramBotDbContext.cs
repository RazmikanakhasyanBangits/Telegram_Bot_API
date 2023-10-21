using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public partial class TelegramBotDbContext : DbContext
    {
        public TelegramBotDbContext()
        {
        }

        public TelegramBotDbContext(DbContextOptions<TelegramBotDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                string connectionString = configuration.GetConnectionString("Default");
                _ = optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BotHistory> BotHistories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<RateModel> Rates { get; set; }
        public DbSet<BankLocation> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            _ = modelBuilder.Entity<BankLocation>(entity =>
            {
                _ = entity.HasIndex(x => x.Id);
                _ = entity.HasOne(x => x.Bank)
                .WithMany(x => x.Locations)
                .HasForeignKey(x => x.BankId);
            });

            _ = modelBuilder.Entity<Bank>(entity =>
            {
                _ = entity.Property(e => e.Id).HasColumnName("ID");

                _ = entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(50);

                _ = entity.Property(e => e.BankUrl)
                    .HasMaxLength(500)
                    .HasColumnName("BankURL");
            });

            _ = modelBuilder.Entity<BotHistory>(entity =>
            {
                _ = entity.ToTable("BotHistory");

                _ = entity.Property(e => e.Id).HasColumnName("ID");
            });

            _ = modelBuilder.Entity<Currency>(entity =>
            {
                _ = entity.HasKey(e => e.Code)
                    .HasName("PK_Currencies_1");

                _ = entity.Property(e => e.Code).HasMaxLength(3);
            });

            _ = modelBuilder.Entity<RateModel>(entity =>
            {
                _ = entity.HasIndex(e => e.FromCurrency, "IX_Rates_FromCurrency");

                _ = entity.HasIndex(e => e.ToCurrency, "IX_Rates_ToCurrency");

                _ = entity.Property(e => e.Id).ValueGeneratedOnAdd();

                _ = entity.Property(e => e.Iteration).IsRequired();

                _ = entity.Property(e => e.BuyValue).HasColumnType("decimal(18, 2)");

                _ = entity.Property(e => e.FromCurrency)
                    .IsRequired()
                    .HasMaxLength(3);

                _ = entity.Property(e => e.SellValue).HasColumnType("decimal(18, 2)");

                _ = entity.Property(e => e.ToCurrency)
                    .IsRequired()
                    .HasMaxLength(3);

                _ = entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rates_Banks");

                _ = entity.HasOne(d => d.FromCurrencyNavigation)
                    .WithMany(p => p.RateFromCurrencyNavigations)
                    .HasForeignKey(d => d.FromCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rates_Currencies");

                _ = entity.HasOne(d => d.ToCurrencyNavigation)
                    .WithMany(p => p.RateToCurrencyNavigations)
                    .HasForeignKey(d => d.ToCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rates_Currencies1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
