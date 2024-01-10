using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IConvertRepository
    {
        Task<List<CurrenciesConvertDetails>> GetConvertDetails(string currency, double exchangedValue);
        Task<FromToConverter> ConvertAsync(string from, string to, decimal amount);
    }
}
