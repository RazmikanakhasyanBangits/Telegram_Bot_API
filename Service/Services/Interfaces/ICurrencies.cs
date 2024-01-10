
using Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces;

public interface ICurrencies
{
    Task<List<CurrenciesConvertDetails>> GetConvertInfoForAllCurrencies(string currency, double exchangedValue);
    Task<FromToConverter> ConvertAsync(string from, string to, decimal amount);
}
