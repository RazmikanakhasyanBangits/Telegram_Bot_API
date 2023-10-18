using DataAccess.Repository;
using System.Collections.Generic;

namespace DataAccess.Models
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
