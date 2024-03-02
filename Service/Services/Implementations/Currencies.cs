using Core.Services.Interfaces;
using Repository.Repositories.Interfaces;
using Service.Model.Infrastructure;
using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Implementations
{
    public class Currencies : ICurrencies
    {
        private readonly IConvertRepository _convertRepository;
        private readonly string _baseCurrency;
        public Currencies(IConvertRepository convertRepository, ISettingsProvider settingsProvider)
        {
            _convertRepository = convertRepository;
            _baseCurrency = settingsProvider.BaseCurrency;
        }

        public async Task<FromToConverter> ConvertAsync(string from, string to, decimal amount)
        {
            from = string.IsNullOrEmpty(from) ? "USD" : from;
            to = string.IsNullOrEmpty(to) ? _baseCurrency : to;
            return await _convertRepository.ConvertAsync(from.Trim(), to.Trim(), amount);
        }

        public async Task<List<CurrenciesConvertDetails>> GetConvertInfoForAllCurrencies(string currency, double exchangedValue)
        {
            return await _convertRepository.GetConvertDetails(currency, exchangedValue);
        }
    }
}
