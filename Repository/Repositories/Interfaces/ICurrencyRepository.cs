using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        public IEnumerable<Currency> All();

    }
}
