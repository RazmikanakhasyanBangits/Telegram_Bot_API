using Service.Model.Models.Currency;
using Service.Model.Models.CurrencySettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces;

public interface ICurrencySettingService
{
    Task ConfigureUserCurrencySettingsAsync(ConfigureUserCurrencySettingsRequest request);
    Task<string> GetUserCurrencyRatesAsync(long userId);
    Task RemoveCurrencyConfigurationAsync(RemoveCurrencyConfigurationRequest request);
}
