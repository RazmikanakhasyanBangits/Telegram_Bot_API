using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Interfaces;
using Service.Model.Models.CurrencySettings;
using Service.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services.Implementations;

public class CurrencySettingServicea(ICurrencySettingsReporisoty currencySettingsReporisoty,
    IUserActivityHistoryRepository userActivityHistoryRepository,IMapper mapper) : ICurrencySettingService
{
    public async Task ConfigureUserCurrencySettingsAsync(ConfigureUserCurrencySettingsRequest request)
    {

        var user = await userActivityHistoryRepository.GetDetailsAsync(x => x.UserExternalId == request.UserId,
            includes:i=>i.Include(_=>_.UserCurrencySettings));

        if (user is null) return;

        user.UserCurrencySettings ??= new List<UserCurrencySetting>();

        try
        {
            if (!user.UserCurrencySettings.Any(x => x.CurrencyFrom == request.CurrencyFrom &&
            x.CurrencyTo == request.CurrencyTo))
            {
                var currencySetting = mapper.Map<UserCurrencySetting>(request);
                currencySetting.UserActivityHistoryId = user.Id;
                user.UserCurrencySettings.Add(currencySetting);

                await userActivityHistoryRepository.UpdateAsync(user);
            };
        }
        catch 
        {
            //Ignore
        }
        

    }
}
