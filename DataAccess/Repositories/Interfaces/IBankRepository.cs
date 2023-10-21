using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBankRepository : IGenericRepository<Bank>
    {
        public IEnumerable<RateModel> All();
    }
}
