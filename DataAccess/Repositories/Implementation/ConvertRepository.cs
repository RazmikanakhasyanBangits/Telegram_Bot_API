using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Shared.Infrastructure;
using Shared.Model;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementation
{
    public class ConvertRepository : IConvertRepository
    {
        private readonly TelegramBotDbContext _dBModel;
        private readonly IBestRatesRepository _bestRateRepository;
        private readonly string _baseCurrency;

        public ConvertRepository(TelegramBotDbContext dBModel, IBestRatesRepository bestRatesRepository,
            ISettingsProvider settingsProvider)
        {
            _dBModel = dBModel;
            _bestRateRepository = bestRatesRepository;
            _baseCurrency = settingsProvider.BaseCurrency;
        }

        public async Task<FromToConverter> ConvertAsync(string from, string to, decimal amount)
        {
            FromToConverter currenciesConvert = new()
            {
                To = to,
                From = from
            };
            IEnumerable<BestRateModel> bests = await _bestRateRepository.GetBestRatesAsync();
            if (to == _baseCurrency)
            {
                if (bests.Count(_ => _.FromCurrency == from) == 0)
                {
                    return currenciesConvert;
                }

                BestRateModel best = bests.Where(_ => _.FromCurrency == from).FirstOrDefault();
                currenciesConvert.Value = best.BuyValue * amount;
                currenciesConvert.BestBank = best.BestBankForBuying;
                currenciesConvert.LastUpdated = best.LastUpdated;
            }
            else if (from == _baseCurrency)
            {
                if (bests.Count(_ => _.FromCurrency == to) == 0)
                {
                    return currenciesConvert;
                }

                BestRateModel best = bests.Where(_ => _.FromCurrency == to).FirstOrDefault();
                currenciesConvert.Value = 1 / best.BuyValue * amount;
                currenciesConvert.BestBank = best.BestBankForBuying;
                currenciesConvert.LastUpdated = best.LastUpdated;
            }
            else
            {
                if (bests.Count(_ => _.FromCurrency == to) == 0)
                {
                    return currenciesConvert;
                }

                decimal fromCurrencyBest = bests.Where(_ => _.FromCurrency == from).FirstOrDefault().BuyValue;
                decimal toCurrencyBest = bests.Where(_ => _.FromCurrency == to).FirstOrDefault().BuyValue;
                currenciesConvert.Value = fromCurrencyBest / toCurrencyBest * amount;
                currenciesConvert.BestBank = bests.Where(_ => _.FromCurrency == from).FirstOrDefault().BestBankForBuying;

                currenciesConvert.LastUpdated = bests.Where(_ => _.FromCurrency == from).FirstOrDefault().LastUpdated;
            }
            return currenciesConvert;

        }

        public async Task<List<CurrenciesConvertDetails>> GetConvertDetails(string currency, double exchangedValue)
        {
            List<CurrenciesConvertDetails> convertDetails = new();
            IQueryable<string> currencies = _dBModel.Currencies.Where(_ => _.Code != currency).Select(_ => _.Code);
            IEnumerable<BestRateModel> bests = await _bestRateRepository.GetBestRatesAsync();

            foreach (string toCurrency in currencies)
            {
                CurrenciesConvertDetails currenciesConvert = new()
                {
                    To = toCurrency
                };

                if (toCurrency == _baseCurrency)
                {
                    if (bests.Count(_ => _.FromCurrency == currency) == 0)
                    {
                        continue;
                    }

                    currenciesConvert.Value = bests.Where(_ => _.FromCurrency == currency).First().BuyValue * (decimal)exchangedValue;
                }
                else if (currency == _baseCurrency)
                {
                    if (bests.Count(_ => _.ToCurrency == currency) == 0)
                    {
                        continue;
                    }

                    if (bests.Count(_ => _.FromCurrency == toCurrency) == 0)
                    {
                        continue;
                    }

                    currenciesConvert.Value = 1 / bests.Where(_ => _.FromCurrency == toCurrency).First().BuyValue * (decimal)exchangedValue;
                }
                else
                {
                    if (bests.Count(_ => _.FromCurrency == toCurrency) == 0)
                    {
                        continue;
                    }

                    decimal fromCurrencyBest = bests.Where(_ => _.FromCurrency == currency).First().BuyValue;
                    decimal toCurrencyBest = bests.Where(_ => _.FromCurrency == toCurrency).First().BuyValue;
                    currenciesConvert.Value = fromCurrencyBest / toCurrencyBest * (decimal)exchangedValue;

                }

                convertDetails.Add(currenciesConvert);

            }

            return convertDetails;
        }
    }
}
