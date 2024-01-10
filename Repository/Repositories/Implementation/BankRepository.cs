using Repository.Entity;
using Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.Implementation
{
    public class BankRepository : GenericRepository<Bank>, IBankRepository
    {
        private readonly TelegramBotDbContext _context;
        public BankRepository(TelegramBotDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<RateModel> All()
        {
            int maxIteration = _context.Rates.Max(_ => _.Iteration);
            return _context.Rates.Include(_ => _.Bank).Where(_ => _.Iteration == maxIteration);
        }
    }
}
