using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces;

public interface IBestRateService
{
    Task<IEnumerable<BestRateModel>> GetBestRatesAsync();
}
