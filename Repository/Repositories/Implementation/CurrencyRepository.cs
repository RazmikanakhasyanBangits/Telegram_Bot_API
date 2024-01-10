using Repository.Repositories.Interfaces;
using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Repositories.Implementation
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly TelegramBotDbContext _context;
        public CurrencyRepository(TelegramBotDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Currency> All()
        {
            return _context.Currencies;
        }

    }
}
