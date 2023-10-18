

using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface ICurrencyRepository
    {
        public IEnumerable<Currency> All();

    }
}
