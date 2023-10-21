﻿using Core.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Shared;
using Shared.Model;
using Shared.Models;
using Shared.Models.Currency;
using Shared.Models.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Implementations
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IBestRateService bestRateService;
        private readonly ICurrencies currencies;
        private readonly ICurrencyService currencyService;
        public BankService(IBankRepository bankRepository, IBestRateService bestRateService, ICurrencyService currencyService, ICurrencies currencies)
        {
            _bankRepository = bankRepository;
            this.bestRateService = bestRateService;
            this.currencyService = currencyService;
            this.currencies = currencies;
        }
        public async Task<string> BestChange(string from, string to, double amount)
        {
            FromToConverter result = await currencies.ConvertAsync(from, to, Convert.ToDecimal(amount));
            StringBuilder builder = new();
            _ = builder.AppendLine($"Արժեք՝ {result.Value:N}{result.To} {result.To.ToFlag()}");
            _ = builder.AppendLine("Բանկը՝ " + result.BestBank);
            _ = builder.AppendLine($"Թարմացվել է` {result.LastUpdated:G}");

            return builder.ToString();
        }
        public IEnumerable<RatesInfoModel> AllRates()
        {
            return _bankRepository.All().Select(MapRates);
        }
        public string GetAllBest()
        {
            Task<IEnumerable<BestRateModel>> data = bestRateService.GetBestRatesAsync();

            Task<IEnumerable<BestRateModel>[]> result = Task.WhenAll(data);
            StringBuilder messageBuilder = new();
            foreach (BestRateModel item in result.Result.FirstOrDefault())
            {
                _ = messageBuilder.AppendLine(item.FromCurrency + item.FromCurrency.ToFlag());
                _ = messageBuilder.AppendLine($"ԱՌՔ: {item.BuyValue:#.##} ({item.BestBankForBuying})");
                _ = messageBuilder.AppendLine($"ՎԱՃԱՌՔ: {item.SellValue:#.##} ({item.BestBankForSelling})");
                _ = messageBuilder.AppendLine($"Թարմացվել է` {item.LastUpdated:G}");
            }


            return messageBuilder.ToString();
        }
        public string GetAll()
        {
            IEnumerable<RatesInfoModel> result = AllRates();
            StringBuilder builder = new();
            foreach (RatesInfoModel item in result)
            {
                _ = builder.AppendLine($"{item.BankName}");

                _ = builder.AppendLine($"{item.FromCurrency}{item.FromCurrency.ToFlag()} ➡️ {item.ToCurrency}{item.ToCurrency.ToFlag()}");
                _ = builder.AppendLine($"{item.BuyValue:#.##} |  {item.SellValue:#.##}");

                _ = builder.AppendLine($"Թարմացվել է` {item.LastUpdated:G}");
                _ = builder.AppendLine("--------------------------------------------");
            }
            return builder.ToString();
        }
        public string GetAvailable()
        {
            IEnumerable<CurrencyModel> result = currencyService.Available();
            StringBuilder builder = new();
            foreach (CurrencyModel item in result)
            {
                _ = builder.AppendLine(item.Code + "-" + item.Description);
            }
            return builder.ToString();
        }
        private RatesInfoModel MapRates(RateModel _rates)
        {
            return new RatesInfoModel
            {
                ID = _rates.Id,
                BankId = _rates.Bank.Id,
                BuyValue = _rates.BuyValue,
                FromCurrency = _rates.FromCurrency,
                LastUpdated = _rates.LastUpdated,
                SellValue = _rates.SellValue,
                ToCurrency = _rates.ToCurrency,
                BankName = _rates.Bank.BankName
            };
        }


    }
}
