using Repository.Entity;
using Service.Model.Models.CurrencySettings;

namespace Service.Profiles;

public class CurrencySettingsProfile:AutoMapper.Profile
{
    public CurrencySettingsProfile()
    {

        CreateMap<UserCurrencySetting, ConfigureUserCurrencySettingsRequest>().ReverseMap();
    }
}
