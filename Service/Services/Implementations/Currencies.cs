using Core.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Implementations
{
    public class Currencies : ICurrencies
    {
        private readonly IConvertRepository _convertRepository;
        public Currencies(IConvertRepository convertRepository)
        {
            _convertRepository = convertRepository;
        }

        public async Task<FromToConverter> ConvertAsync(string from, string to, decimal amount)
        {
            return await _convertRepository.ConvertAsync(from, to, amount);
        }

        public async Task<List<CurrenciesConvertDetails>> GetConvertInfoForAllCurrencies(string currency, double exchangedValue)
        {
            return await _convertRepository.GetConvertDetails(currency, exchangedValue);
        }
    }
}
