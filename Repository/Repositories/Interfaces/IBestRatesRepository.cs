using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IBestRatesRepository
    {
        Task<IEnumerable<BestRateModel>> GetBestRatesAsync();
    }
}
