using Shared.Models.Rates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IBankService
    {
        public IEnumerable<RatesInfoModel> AllRates();
        Task<string> BestChange(string from, string to, double amount);
        string GetAll();
        string GetAllBest();
        string GetAvailable();
    }
}
