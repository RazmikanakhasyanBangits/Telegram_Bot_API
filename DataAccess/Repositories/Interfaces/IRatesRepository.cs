using DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRatesRepository
    {
         Task BulkInsert(IEnumerable<RateModel> rate);
    }
}
