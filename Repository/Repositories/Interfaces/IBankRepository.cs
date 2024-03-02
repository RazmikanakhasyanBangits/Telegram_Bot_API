using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Repositories.Interfaces
{
    public interface IBankRepository : IGenericRepository<Bank>
    {
        public IEnumerable<RateModel> All();
    }
}
