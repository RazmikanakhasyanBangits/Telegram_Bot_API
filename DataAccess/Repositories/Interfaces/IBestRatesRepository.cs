using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBestRatesRepository
    {
        Task<IEnumerable<BestRateModel>> GetBestRatesAsync();
    }
}
