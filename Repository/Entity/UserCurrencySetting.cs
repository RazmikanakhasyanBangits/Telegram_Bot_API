using Microsoft.EntityFrameworkCore;

namespace Repository.Entity;

public class UserCurrencySetting :BaseEntity<long>
{
    public string CurrencyFrom { get; set; }
    public string CurrencyTo { get; set; }
    public int Position { get; set; }

    public long UserActivityHistoryId { get; set; }
    public UserActivityHistory UserActivityHistory { get; set; }
}

public static class UserCurrencySettingEfConfig
{
    public static void AddUserCurrencySettingEfConfig(this ModelBuilder builder)
    {
        builder.Entity<UserCurrencySetting>(opt => {

            opt.HasIndex(x => new { x.CurrencyTo, x.CurrencyFrom, x.UserActivityHistoryId });

            opt.HasOne(x => x.UserActivityHistory)
            .WithMany(x => x.UserCurrencySettings)
            .HasForeignKey(x => x.UserActivityHistoryId);

            opt.Property(x => x.CurrencyFrom).HasMaxLength(5);
            opt.Property(x=>x.CurrencyTo).HasMaxLength(5);
        });
    }
}