using DataAccess.Entity;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementation
{
    public class RatesRepository : IRatesRepository
    {
        private readonly TelegramBotDbContext _db;
        public RatesRepository(TelegramBotDbContext db)
        {
            _db = db;
        }
        public async Task BulkInsert(IEnumerable<RateModel> rate)
        {
            var cc = _db.Rates.Count();
            var maxIteration =  _db.Rates.Count() == 0 ? 1 : _db.Rates.Max(_ => _.Iteration) + 1;
            var availables = _db.Currencies.Select(_ => _.Code).ToList();
            var ratesToAdd = rate.Where(_ => availables.Contains(_.FromCurrency))
                .GroupBy(_ => $"{_.BankId}{_.FromCurrency}")
                .Select(_ => _.First())
                .ToList();
            
            foreach (var item in ratesToAdd)
            {
                item.LastUpdated = DateTime.Now;
                item.Iteration = maxIteration;
            }

            _db.Rates.AddRange(ratesToAdd);
            await _db.SaveChangesAsync();
        }
    }
}
