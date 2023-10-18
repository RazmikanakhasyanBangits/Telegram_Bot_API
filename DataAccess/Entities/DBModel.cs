using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    class DBModel:DbContext
    {
        public DBModel()
        {

        }
        public DBModel(DbContextOptions<DBModel> options)
            : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BotHistory> BotHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LT0004573\TECH42; initial catalog = Tech42_TelegramBotDB; integrated security = True; MultipleActiveResultSets = True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Currency>
                (_ => { 
                    _.Property(_c => _c.ID).IsRequired().ValueGeneratedOnAdd();
                    _.Property(_c => _c.Code).IsRequired().HasMaxLength(3);
                    _.Property(_c => _c.Code).HasMaxLength(50);
                }) ;
            modelBuilder.Entity<Currency>().HasKey(_ => _.ID);

            modelBuilder.Entity<Bank>
             (_ => {
                 _.Property(_b => _b.ID).IsRequired().ValueGeneratedOnAdd();
                 _.Property(_b => _b.BankName).IsRequired().HasMaxLength(50);
                 _.Property(_b => _b.BankURL).HasMaxLength(500);
             });
            modelBuilder.Entity<Bank>().HasKey(_ => _.ID);


            foreach (var prop in typeof(Rate).GetProperties())
            {
                modelBuilder
                    .Entity<Rate>()
                    .Property(prop.Name)
                    .IsRequired();
            }
            modelBuilder.Entity<Rate>
                (_ => {
                    _.Property(_r => _r.ID).ValueGeneratedOnAdd();
                    _.Property(_r => _r.FromCurrency).HasMaxLength(3);
                    _.Property(_r => _r.ToCurrency).HasMaxLength(3);
                    _.Property(_r => _r.LastUpdated).HasColumnType("datetime2");
                });

            modelBuilder.Entity<Rate>().HasKey(_ => _.ID);
            modelBuilder.Entity<Rate>().HasIndex(_ => _.FromCurrency);
            modelBuilder.Entity<Rate>().HasIndex(_ => _.ToCurrency);

            //modelBuilder.Entity<Bank>().HasMany(_ => _.Rates)
            //    .WithOne(_ => _.Bank);

            //modelBuilder.Entity<Rate>().HasOne(_ => _.FromCurrency);
            //modelBuilder.Entity<Rate>().HasOne(_ => _.ToCurrency);


            modelBuilder.Entity<BotHistory>
            (_ => {
                _.Property(_b => _b.ID).IsRequired().ValueGeneratedOnAdd();
                _.Property(_b => _b.Date).IsRequired().HasColumnType("datetime2");

            });
            modelBuilder.Entity<BotHistory>().HasKey(_ => _.ID);

         



        }
    }
}
