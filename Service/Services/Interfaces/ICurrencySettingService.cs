using Service.Model.Models.CurrencySettings;
using System.Threading.Tasks;

namespace Service.Services.Interfaces;

public interface ICurrencySettingService
{
    Task ConfigureUserCurrencySettingsAsync(ConfigureUserCurrencySettingsRequest request);
}
