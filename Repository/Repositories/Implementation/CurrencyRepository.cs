using Repository.Repositories.Interfaces;
using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Repositories.Implementation
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ExchangeBotDbContext _context;
        public CurrencyRepository(ExchangeBotDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Currency> All()
        {
            return _context.Currencies;
        }

    }
}
