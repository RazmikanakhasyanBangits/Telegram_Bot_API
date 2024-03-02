using AutoMapper;
using Azure.Core;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Entity;
using Repository.Repositories.Interfaces;
using Service.Model.Models.Currency;
using Service.Model.Models.CurrencySettings;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests.Abstractions;

namespace Service.Services.Implementations;

public class CurrencySettingServicea(ICurrencySettingsReporisoty currencySettingsReporisoty,
    IUserActivityHistoryRepository userActivityHistoryRepository, IMapper mapper,
    ICurrencies currencies, ICurrencyRepository currencyRepository) : ICurrencySettingService
{
    public async Task ConfigureUserCurrencySettingsAsync(ConfigureUserCurrencySettingsRequest request)
    {

        var user = await userActivityHistoryRepository.GetDetailsAsync(x => x.UserExternalId == request.UserId,
            includes: i => i.Include(_ => _.UserCurrencySettings));

        if (user is null) return;
        var isFromValid =(await currencyRepository.GetAllAsync(x => x.Code == request.CurrencyFrom)).ToList();
        var isToValid = (await currencyRepository.GetAllAsync(x => x.Code == request.CurrencyTo)).ToList();

        if (isFromValid is { Count:0}  || isToValid is { Count:0} ) return;

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
    public async Task RemoveCurrencyConfigurationAsync(RemoveCurrencyConfigurationRequest request)
    {
        var user = await userActivityHistoryRepository.GetDetailsAsync(x => x.UserExternalId == request.UserId,
          includes: i => i.Include(_ => _.UserCurrencySettings));

        if (user is null) return;

        if (user.UserCurrencySettings.Count is 0) return;

        user.UserCurrencySettings.RemoveAll(x => x.CurrencyFrom == request.CurrencyFrom && x.CurrencyTo == request.CurrencyTo);

        await userActivityHistoryRepository.UpdateAsync(user);
    }

    public async Task<string> GetUserCurrencyRatesAsync(long userId)
    {
        var user = await userActivityHistoryRepository.GetDetailsAsync(x => x.UserExternalId == userId,
          includes: i => i.Include(_ => _.UserCurrencySettings));

        if (user is null) return null;

        if (user.UserCurrencySettings is null || user.UserCurrencySettings is { Count: 0 }) return null;
        var results = new List<string>();

        
        var result =  user.UserCurrencySettings.Select(x => new StringBuilder
       (
           $"From:{x.CurrencyFrom}\n" +
           $"To:{x.CurrencyTo}\n" +
           $"Value:{Math.Round(Convert.ToDecimal(currencies.ConvertAsync(x.CurrencyFrom, x.CurrencyTo, 1).Result.Value),2)}\n" +
           $"BestBank:{ currencies.ConvertAsync(x.CurrencyFrom, x.CurrencyTo, 1).Result.BestBank}\n" +
           $"------------------------------------------\n"
       )).Select(x=>x.ToString());

        return string.Join("", result);

    }
}
