using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Banks;
using Shared.Models.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly TelegramBotDbContext _context;
        public BankRepository(TelegramBotDbContext context)
        {
            _context = context;
        }

        public IEnumerable<RateModel> All()
        {
            var maxIteration = _context.Rates.Max(_ => _.Iteration);
            return _context.Rates.Include(_ => _.Bank).Where(_ => _.Iteration == maxIteration);
        }
    }
}
