using Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IConvertRepository
    {
        Task<List<CurrenciesConvertDetails>> GetConvertDetails(string currency, double exchangedValue);
        Task<FromToConverter> ConvertAsync(string from, string to, decimal amount);
    }
}
