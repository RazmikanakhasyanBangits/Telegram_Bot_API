using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace DataAccess.Repositories.Implementation
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
