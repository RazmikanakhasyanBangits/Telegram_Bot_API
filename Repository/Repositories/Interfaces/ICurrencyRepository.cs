using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Repositories.Interfaces
{
    public interface ICurrencyRepository : IGenericRepository<Currency>
    {
        public IEnumerable<Currency> All();

    }
}
