using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IRateService
    {
        Task BulkInsert(IEnumerable<RateModel> rate);
    }
}
