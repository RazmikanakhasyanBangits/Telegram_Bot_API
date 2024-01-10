using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Shared.Infrastructure;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementation
{
    public class BestRatesRepository : IBestRatesRepository
    {
        private readonly TelegramBotDbContext _dBModel;
        private readonly string _baseCurrency;
        public BestRatesRepository(TelegramBotDbContext dBModel, ISettingsProvider settingsProvider)
        {
            _dBModel = dBModel;
            _baseCurrency = settingsProvider.BaseCurrency;
        }
        public async Task<IEnumerable<BestRateModel>> GetBestRatesAsync()
        {

            List<BestRateModel> bestRates = new();
            List<string> currencies = await _dBModel.Currencies
                .Where(x => x.Code != _baseCurrency)
                .Select(_ => _.Code).ToListAsync();

            int lastIteration = _dBModel.Rates.Max(_ => _.Iteration);
            IQueryable<RateModel> lastRates = _dBModel.Rates.Where(_ => _.Iteration == lastIteration);


            foreach (string currency in currencies)
            {
                BestRateModel bestRateModel = new();

                IIncludableQueryable<RateModel, Bank> currencyRate = lastRates
                           .Where(_ => _.FromCurrency == currency && _.ToCurrency == _baseCurrency)
                           .Include(_ => _.Bank);

                RateModel currencyRateBuy = await currencyRate
                           .OrderByDescending(_ => _.BuyValue)
                           .FirstOrDefaultAsync();

                RateModel currencyRateSell = await currencyRate
                    .OrderBy(_ => _.SellValue)
                    .FirstOrDefaultAsync();
                if (currencyRateBuy != null)
                {
                    bestRateModel.BestBankForBuying = currencyRateBuy.Bank.BankName;
                    bestRateModel.BuyValue = currencyRateBuy.BuyValue;
                    bestRateModel.LastUpdated = currencyRateBuy.LastUpdated;

                }

                if (currencyRateSell != null)
                {
                    bestRateModel.BestBankForSelling = currencyRateSell.Bank.BankName;
                    bestRateModel.SellValue = currencyRateSell.SellValue;
                    bestRateModel.LastUpdated = currencyRateSell.LastUpdated;
                }

                if (currencyRateBuy != null || currencyRateSell != null)
                {
                    bestRateModel.FromCurrency = currency;
                    bestRateModel.ToCurrency = _baseCurrency;
                    bestRates.Add(bestRateModel);
                }
            }
            return bestRates;
        }
    }
}
