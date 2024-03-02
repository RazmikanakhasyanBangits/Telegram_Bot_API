using Repository.Entity;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementation;

public class CurrencySettingsRepository : GenericRepository<UserCurrencySetting>, ICurrencySettingsReporisoty
{
    public CurrencySettingsRepository(ExchangeBotDbContext context) : base(context) { }
}
