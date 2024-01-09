using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces;

public interface IBestRateService
{
    Task<IEnumerable<BestRateModel>> GetBestRatesAsync();
}
