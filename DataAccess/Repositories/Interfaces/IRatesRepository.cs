using DataAccess.Entity;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRatesRepository
    {
         Task BulkInsert(IEnumerable<RateModel> rate);
    }
}
