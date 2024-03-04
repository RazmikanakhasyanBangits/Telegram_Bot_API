using Service.Model.Models.Rates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces;

public interface IBankService
{
    public IEnumerable<RatesInfoModel> AllRates();
    Task<string> BestChange(string from, string to, double amount);
    string GetAll();
    string GetAllBest();
    Task<string> GetAllBestDistances(double latitude, double longitude);
    string GetAvailable();
}
