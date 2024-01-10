using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IRatesRepository
    {
         Task BulkInsert(IEnumerable<RateModel> rate);
    }
}
